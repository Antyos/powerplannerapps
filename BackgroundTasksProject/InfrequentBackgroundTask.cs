﻿using PowerPlannerAppDataLibrary.App;
using PowerPlannerAppDataLibrary.DataLayer;
using PowerPlannerAppDataLibrary.Extensions;
using PowerPlannerUWPLibrary;
using PowerPlannerUWPLibrary.TileHelpers;
using System;
using System.Threading;
using Windows.ApplicationModel.Background;

namespace BackgroundTasksProject
{
    public sealed class InfrequentBackgroundTask : IBackgroundTask
    {
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            SharedInitialization.Initialize();
            InitializeUWP.Initialize();

            taskInstance.Canceled += TaskInstance_Canceled;

            var deferral = taskInstance.GetDeferral();

            try
            {
                var accounts = await AccountsManager.GetAllAccounts();

                var cancellationToken = _cancellationTokenSource.Token;

                try
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    foreach (var a in accounts)
                    {
                        var data = await AccountDataStore.Get(a.LocalAccountId);

                        cancellationToken.ThrowIfCancellationRequested();

                        await TileHelper.UpdateTileNotificationsForAccountAsync(a, data);

                        cancellationToken.ThrowIfCancellationRequested();
                    }
                }

                catch (OperationCanceledException) { }
            }

            catch (Exception ex)
            {
                TelemetryExtension.Current?.TrackException(ex);
            }

            // TODO: Re-schedule toast notifications?

            finally
            {
                deferral.Complete();
            }
        }

        private void TaskInstance_Canceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            _cancellationTokenSource.Cancel();
        }
    }
}
