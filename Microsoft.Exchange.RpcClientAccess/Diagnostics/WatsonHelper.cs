using System;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Diagnostics
{
	// Token: 0x02000042 RID: 66
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class WatsonHelper
	{
		// Token: 0x06000278 RID: 632 RVA: 0x00008D88 File Offset: 0x00006F88
		public static WatsonReportAction RegisterWatsonReportDataProvider(this Activity activity, WatsonReportActionType reportActionType, WatsonHelper.IProvideWatsonReportData evaluationDelegate)
		{
			WatsonReportAction watsonReportAction = WatsonHelper.CreateWatsonReportDataProvider(activity, reportActionType, evaluationDelegate);
			activity.RegisterWatsonReportAction(watsonReportAction);
			return watsonReportAction;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00008DA6 File Offset: 0x00006FA6
		public static IDisposable RegisterWatsonReportDataProviderAndGetGuard(this Activity activity, WatsonReportActionType reportActionType, WatsonHelper.IProvideWatsonReportData evaluationDelegate)
		{
			return WatsonHelper.ReportActionGuard.Create(activity, WatsonHelper.CreateWatsonReportDataProvider(activity, reportActionType, evaluationDelegate));
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600027A RID: 634 RVA: 0x00008DB6 File Offset: 0x00006FB6
		private static string AppName
		{
			get
			{
				if (WatsonHelper.appName == null)
				{
					WatsonHelper.appName = ExWatson.AppName;
				}
				return WatsonHelper.appName;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600027B RID: 635 RVA: 0x00008DD0 File Offset: 0x00006FD0
		private static string AppVersion
		{
			get
			{
				if (WatsonHelper.appVersion == null)
				{
					using (Process currentProcess = Process.GetCurrentProcess())
					{
						Version version;
						if (ExWatson.TryGetRealApplicationVersion(currentProcess, out version))
						{
							WatsonHelper.appVersion = version.ToString();
						}
						else
						{
							WatsonHelper.appVersion = "0";
						}
					}
				}
				return WatsonHelper.appVersion;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600027C RID: 636 RVA: 0x00008E2C File Offset: 0x0000702C
		private static bool IsTestEnvironment
		{
			get
			{
				if (WatsonHelper.isTestEnvironment == null)
				{
					string environmentVariable = Environment.GetEnvironmentVariable("USERDNSDOMAIN");
					WatsonHelper.isTestEnvironment = new bool?(environmentVariable != null && environmentVariable.EndsWith(".EXTEST.MICROSOFT.COM", StringComparison.OrdinalIgnoreCase));
				}
				return WatsonHelper.isTestEnvironment.Value;
			}
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00008E78 File Offset: 0x00007078
		public static void SendGenericWatsonReport(string exceptionType, string exceptionDetails)
		{
			Util.ThrowOnNullOrEmptyArgument(exceptionType, "exceptionType");
			if (WatsonHelper.IsTestEnvironment)
			{
				return;
			}
			StackTrace stackTrace = new StackTrace(1);
			MethodBase method = stackTrace.GetFrame(0).GetMethod();
			AssemblyName name = method.DeclaringType.Assembly.GetName();
			int hashCode = (method.Name + exceptionType).GetHashCode();
			ExWatson.SendGenericWatsonReport("E12", WatsonHelper.AppVersion, WatsonHelper.AppName, name.Version.ToString(), name.Name, exceptionType, stackTrace.ToString(), hashCode.ToString("x"), method.Name, exceptionDetails);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00008F0E File Offset: 0x0000710E
		private static bool IsActionEnabled(string actionName)
		{
			return true;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00008F14 File Offset: 0x00007114
		private static WatsonReportAction CreateWatsonReportDataProvider(Activity activity, WatsonReportActionType reportActionType, WatsonHelper.IProvideWatsonReportData evaluationDelegate)
		{
			string actionName = WatsonHelper.ReportActionTypeToActionName(reportActionType);
			if (!WatsonHelper.IsActionEnabled(actionName))
			{
				return null;
			}
			return new WatsonHelper.DelegateWatsonReportAction(actionName, evaluationDelegate);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00008F3C File Offset: 0x0000713C
		private static string ReportActionTypeToActionName(WatsonReportActionType reportActionType)
		{
			switch (reportActionType)
			{
			case WatsonReportActionType.Connection:
				return "Connection";
			case WatsonReportActionType.IcsDownload:
				return "ICS Download";
			case WatsonReportActionType.MessageAdaptor:
				return "ICS/FX Message";
			case WatsonReportActionType.FolderAdaptor:
				return "ICS/FX Folder";
			case WatsonReportActionType.FastTransferState:
				return "ICS/FX client/processing state";
			default:
				throw new ArgumentOutOfRangeException("reportActionType");
			}
		}

		// Token: 0x040001EC RID: 492
		private static string appName;

		// Token: 0x040001ED RID: 493
		private static string appVersion;

		// Token: 0x040001EE RID: 494
		private static bool? isTestEnvironment;

		// Token: 0x02000043 RID: 67
		public interface IProvideWatsonReportData
		{
			// Token: 0x06000281 RID: 641
			string GetWatsonReportString();
		}

		// Token: 0x02000044 RID: 68
		private sealed class ReportActionGuard : BaseObject
		{
			// Token: 0x06000282 RID: 642 RVA: 0x00008F94 File Offset: 0x00007194
			private ReportActionGuard(Activity activity, WatsonReportAction reportAction)
			{
				using (DisposeGuard disposeGuard = this.Guard())
				{
					Util.ThrowOnNullArgument(reportAction, "reportAction");
					this.activity = activity;
					this.reportAction = reportAction;
					this.activity.RegisterWatsonReportAction(this.reportAction);
					disposeGuard.Success();
				}
			}

			// Token: 0x06000283 RID: 643 RVA: 0x00009000 File Offset: 0x00007200
			public static IDisposable Create(Activity activity, WatsonReportAction reportAction)
			{
				if (reportAction == null || !WatsonHelper.IsActionEnabled(reportAction.ActionName))
				{
					return null;
				}
				return new WatsonHelper.ReportActionGuard(activity, reportAction);
			}

			// Token: 0x06000284 RID: 644 RVA: 0x0000901B File Offset: 0x0000721B
			protected override void InternalDispose()
			{
				this.activity.UnregisterWatsonReportAction(this.reportAction);
				base.InternalDispose();
			}

			// Token: 0x06000285 RID: 645 RVA: 0x00009034 File Offset: 0x00007234
			protected override DisposeTracker GetDisposeTracker()
			{
				return DisposeTracker.Get<WatsonHelper.ReportActionGuard>(this);
			}

			// Token: 0x040001EF RID: 495
			private readonly Activity activity;

			// Token: 0x040001F0 RID: 496
			private readonly WatsonReportAction reportAction;
		}

		// Token: 0x02000045 RID: 69
		private sealed class DelegateWatsonReportAction : WatsonReportAction
		{
			// Token: 0x06000286 RID: 646 RVA: 0x0000903C File Offset: 0x0000723C
			public DelegateWatsonReportAction(string actionName, WatsonHelper.IProvideWatsonReportData dataProvider) : base(null, true)
			{
				Util.ThrowOnNullArgument(dataProvider, "dataProvider");
				this.actionName = actionName;
				this.dataProvider = dataProvider;
			}

			// Token: 0x17000083 RID: 131
			// (get) Token: 0x06000287 RID: 647 RVA: 0x0000905F File Offset: 0x0000725F
			public override string ActionName
			{
				get
				{
					return this.actionName;
				}
			}

			// Token: 0x06000288 RID: 648 RVA: 0x00009068 File Offset: 0x00007268
			public override string Evaluate(WatsonReport watsonReport)
			{
				string result;
				try
				{
					result = (this.dataProvider.GetWatsonReportString() ?? string.Empty);
				}
				catch (Exception ex)
				{
					result = ex.ToString();
				}
				return result;
			}

			// Token: 0x06000289 RID: 649 RVA: 0x000090A8 File Offset: 0x000072A8
			public override bool Equals(object obj)
			{
				WatsonHelper.DelegateWatsonReportAction delegateWatsonReportAction = obj as WatsonHelper.DelegateWatsonReportAction;
				return delegateWatsonReportAction != null && this.dataProvider.Equals(delegateWatsonReportAction.dataProvider);
			}

			// Token: 0x0600028A RID: 650 RVA: 0x000090D2 File Offset: 0x000072D2
			public override int GetHashCode()
			{
				return this.dataProvider.GetHashCode();
			}

			// Token: 0x040001F1 RID: 497
			private readonly string actionName;

			// Token: 0x040001F2 RID: 498
			private readonly WatsonHelper.IProvideWatsonReportData dataProvider;
		}
	}
}
