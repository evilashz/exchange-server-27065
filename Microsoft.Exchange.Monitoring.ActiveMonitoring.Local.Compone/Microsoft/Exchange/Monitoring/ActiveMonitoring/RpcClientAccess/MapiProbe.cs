using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.MapiHttp;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Monitoring;
using Microsoft.Mapi;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.RpcClientAccess
{
	// Token: 0x020001F4 RID: 500
	internal abstract class MapiProbe : ProbeWorkItem
	{
		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000DF2 RID: 3570 RVA: 0x0005E747 File Offset: 0x0005C947
		// (set) Token: 0x06000DF3 RID: 3571 RVA: 0x0005E74F File Offset: 0x0005C94F
		internal NetworkCredential ServerToServerCredential { private get; set; }

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000DF4 RID: 3572 RVA: 0x0005E758 File Offset: 0x0005C958
		// (set) Token: 0x06000DF5 RID: 3573 RVA: 0x0005E760 File Offset: 0x0005C960
		private protected IContext Context { protected get; private set; }

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000DF6 RID: 3574 RVA: 0x0005E769 File Offset: 0x0005C969
		// (set) Token: 0x06000DF7 RID: 3575 RVA: 0x0005E771 File Offset: 0x0005C971
		private protected ITask Task { protected get; private set; }

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000DF8 RID: 3576 RVA: 0x0005E77A File Offset: 0x0005C97A
		// (set) Token: 0x06000DF9 RID: 3577 RVA: 0x0005E782 File Offset: 0x0005C982
		private protected new InterpretedResult Result { protected get; private set; }

		// Token: 0x06000DFA RID: 3578 RVA: 0x0005E78C File Offset: 0x0005C98C
		protected override void DoWork(CancellationToken cancellationToken)
		{
			this.ConfigureTask();
			this.Result = this.CreateInterpretedResult();
			this.Result.RawResult = base.Result;
			TplTaskEngine.BeginExecute(this.Task, cancellationToken).ContinueWith(new Action<Task>(this.TranslateTaskResults), TaskContinuationOptions.AttachedToParent);
		}

		// Token: 0x06000DFB RID: 3579
		protected abstract ITask CreateTask();

		// Token: 0x06000DFC RID: 3580 RVA: 0x0005E7DB File Offset: 0x0005C9DB
		protected virtual bool ShouldCreateRestrictedCredentials()
		{
			return false;
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x0005E7DE File Offset: 0x0005C9DE
		protected virtual InterpretedResult CreateInterpretedResult()
		{
			return new InterpretedResult();
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x0005E7E8 File Offset: 0x0005C9E8
		protected MapiProbe()
		{
			this.AddOrUpdateRootCauseToComponentMapping("UnknownIssue", FailingComponent.Momt);
			this.AddOrUpdateRootCauseToComponentMapping("HighLatency", FailingComponent.Momt);
			this.AddOrUpdateRootCauseToComponentMapping("Passive", FailingComponent.ActiveManager);
			this.AddOrUpdateRootCauseToComponentMapping("SecureChannel", FailingComponent.Directory);
			this.AddOrUpdateRootCauseToComponentMapping("Networking", FailingComponent.Networking);
			this.AddOrUpdateRootCauseToComponentMapping("AccountIssue", FailingComponent.Directory);
			this.AddOrUpdateRootCauseToComponentMapping("Unauthorized", FailingComponent.Directory);
			this.AddOrUpdateRootCauseToComponentMapping("MapiHttpVersionMismatch", FailingComponent.Momt);
			this.AddOrUpdateRootCauseToComponentMapping("StoreFailure", FailingComponent.Store);
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x0005E89C File Offset: 0x0005CA9C
		protected virtual void ProcessTaskException(Exception ex)
		{
			WebException ex2 = ex as WebException;
			if (ex is CallCancelledException || (ex2 != null && ex2.Status == WebExceptionStatus.RequestCanceled))
			{
				this.SetRootCause("UnknownIssue");
			}
			else if (ex.GetBaseException().Message.Contains("prm[2]: Long val: 1703 (0x000006A7)"))
			{
				this.SetRootCause("SecureChannel");
			}
			else if (ex.GetBaseException() is SocketException || (ex2 != null && ex2.Status == WebExceptionStatus.NameResolutionFailure))
			{
				this.SetRootCause("Networking");
			}
			else if (MapiProbe.DidProbeFailDueToStoreIssue(ex))
			{
				this.SetRootCause("StoreFailure");
			}
			else if (this.DidProbeFailDueToAccountIssue(ex))
			{
				this.SetRootCause("AccountIssue");
			}
			else if (this.DidProbeFailDueToCredentialIssue(ex))
			{
				this.SetRootCause("Unauthorized");
			}
			throw new AggregateException(new Exception[]
			{
				ex
			});
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x0005E96E File Offset: 0x0005CB6E
		protected virtual void ConfigureProbeTaskTimeout()
		{
			this.Context.Properties.Set(ContextPropertySchema.Timeout, TimeSpan.FromSeconds((double)base.Definition.TimeoutSeconds));
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x0005E99B File Offset: 0x0005CB9B
		protected void SetRootCause(string rootCause)
		{
			this.Result.SetRootCause(rootCause, this.RootCauseToComponentMapping.GetValueOrDefault(rootCause, FailingComponent.Momt));
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x0005E9B6 File Offset: 0x0005CBB6
		protected void AddOrUpdateRootCauseToComponentMapping(string rootCauseName, FailingComponent component)
		{
			ArgumentValidator.ThrowIfNull("rootCauseName", rootCauseName);
			this.RootCauseToComponentMapping[rootCauseName] = component;
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x0005E9D0 File Offset: 0x0005CBD0
		protected bool DidProbeFailDueToInvalidRequestType(Exception ex)
		{
			InvalidRequestTypeException ex2 = ex as InvalidRequestTypeException;
			if (ex2 != null && ex2.ResponseCode == ResponseCode.InvalidRequestType)
			{
				if (ex2.MapiHttpVersion == null)
				{
					return true;
				}
				if (ex2.MapiHttpVersion < this.mapiHttpVerbsUpdateVersion)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x0005EA18 File Offset: 0x0005CC18
		protected bool DidProbeFailDueToDatabaseMountedElsewhere(Exception ex)
		{
			WebException ex2 = ex as WebException;
			ServerUnavailableException ex3 = ex as ServerUnavailableException;
			return (ex2 != null && this.Context.Properties.GetNullableOrDefault(ContextPropertySchema.ResponseStatusCode) == (HttpStatusCode)555) || (ex3 != null && ex.GetBaseException().Message.Contains("prm[0]: Long val: 555 (0x0000022B)")) || MapiProbe.DidProbeFailDueToPassiveMDB(ex);
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x0005EA90 File Offset: 0x0005CC90
		protected bool DidProbeFailDueToCredentialIssue(Exception ex)
		{
			WebException ex2 = ex as WebException;
			RpcException ex3 = ex as RpcException;
			if (ex2 == null && ex3 == null)
			{
				ex2 = (ex.GetBaseException() as WebException);
			}
			return (ex2 != null && this.Context.Properties.GetNullableOrDefault(ContextPropertySchema.ResponseStatusCode) == HttpStatusCode.Unauthorized) || (ex3 != null && ex.GetBaseException().Message.Contains("Status: 0x00000005"));
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x0005EB10 File Offset: 0x0005CD10
		protected bool DidProbeFailDueToAccountIssue(Exception ex)
		{
			WebException ex2 = ex as WebException;
			if (ex2 != null)
			{
				HttpStatusCode value = this.Context.Properties.GetNullableOrDefault(ContextPropertySchema.ResponseStatusCode).Value;
				if (value == (HttpStatusCode)456 || value == (HttpStatusCode)457)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x0005EB58 File Offset: 0x0005CD58
		protected static bool DidProbeFailDueToStoreIssue(Exception ex)
		{
			return MapiProbe.SearchExceptionString(ex, MapiProbe.KnownStoreMailboxExceptions);
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x0005EB65 File Offset: 0x0005CD65
		protected static bool DidProbeFailDueToPassiveMDB(Exception ex)
		{
			return MapiProbe.SearchExceptionString(ex, MapiProbe.KnownStorePassiveMDBExceptions);
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x0005EB88 File Offset: 0x0005CD88
		protected static bool SearchExceptionString(Exception exception, string[] expectedExceptionsNames)
		{
			string exceptionString = exception.ToString();
			return expectedExceptionsNames.Any((string exceptionName) => exceptionString.Contains(exceptionName));
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x0005EBB9 File Offset: 0x0005CDB9
		private void ConfigureTask()
		{
			this.CreateContext();
			this.ConfigureEndpoint();
			this.ConfigureProbeTaskTimeout();
			this.ConfigureAuthentication();
			this.ConfigureEmsmdbParameters();
			this.Task = this.CreateTask();
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x0005EBE8 File Offset: 0x0005CDE8
		private void CreateContext()
		{
			this.Context = Microsoft.Exchange.RpcClientAccess.Monitoring.Context.CreateRoot(new Environment(), new CompositeLogger
			{
				this.outlineLogger,
				this.briefLogger
			});
			this.Context.Properties.Set(ContextPropertySchema.AdditionalHttpHeaders, new WebHeaderCollection());
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x0005EC64 File Offset: 0x0005CE64
		private void ConfigureAuthentication()
		{
			if (base.Definition.AccountPassword != null)
			{
				RpcProxyPort asEnum = this.GetAsEnum<RpcProxyPort>("RpcProxyPort", Microsoft.Exchange.Rpc.RpcProxyPort.Default);
				this.Context.Properties.Set(ContextPropertySchema.RpcProxyPort, asEnum);
				if (asEnum.Equals(Microsoft.Exchange.Rpc.RpcProxyPort.Backend))
				{
					this.PopulateAdditionalHttpHeaders("X-RpcHttpProxyServerTarget", base.Definition.Attributes["PersonalizedServerName"]);
				}
				HttpAuthenticationScheme targetAuthenticationScheme = this.GetAsEnum<HttpAuthenticationScheme>("RpcProxyAuthenticationType", HttpAuthenticationScheme.Basic);
				this.Context.Properties.Set(ContextPropertySchema.RpcProxyAuthenticationType, targetAuthenticationScheme);
				this.Context.Properties.Set(ContextPropertySchema.RpcAuthenticationType, this.GetAsEnum<AuthenticationService>("RpcAuthenticationType", AuthenticationService.None));
				ICredentials credentials = new NetworkCredential(base.Definition.Account, base.Definition.AccountPassword);
				if (targetAuthenticationScheme == HttpAuthenticationScheme.Basic && this.ShouldCreateRestrictedCredentials())
				{
					credentials = new RestrictedCredentials(credentials, (string requestedAuthType) => StringComparer.OrdinalIgnoreCase.Equals(requestedAuthType, targetAuthenticationScheme.ToString()));
				}
				this.Context.Properties.Set(ContextPropertySchema.Credentials, credentials);
				return;
			}
			this.Context.Properties.Set(ContextPropertySchema.Credentials, this.ServerToServerCredential);
			this.PopulateAdditionalHttpHeaders("X-CommonAccessToken", base.Definition.Account);
			this.PopulateAdditionalHttpHeaders("X-RpcHttpProxyServerTarget", base.Definition.Attributes["PersonalizedServerName"]);
			this.Context.Properties.Set(ContextPropertySchema.RpcProxyPort, Microsoft.Exchange.Rpc.RpcProxyPort.Backend);
			this.Context.Properties.Set(ContextPropertySchema.RpcProxyAuthenticationType, HttpAuthenticationScheme.Negotiate);
			this.Context.Properties.Set(ContextPropertySchema.RpcAuthenticationType, AuthenticationService.None);
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x0005EE48 File Offset: 0x0005D048
		private void PopulateAdditionalHttpHeaders(string headerName, string headerValue)
		{
			WebHeaderCollection webHeaderCollection = this.Context.Properties.Get(ContextPropertySchema.AdditionalHttpHeaders);
			webHeaderCollection.Add(headerName, headerValue);
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x0005EE74 File Offset: 0x0005D074
		private void ConfigureEmsmdbParameters()
		{
			string text = base.Definition.Attributes["AccountLegacyDN"];
			this.Context.Properties.Set(ContextPropertySchema.UserLegacyDN, text);
			this.Context.Properties.Set(ContextPropertySchema.MailboxLegacyDN, base.Definition.Attributes.GetValueOrDefault("MailboxLegacyDN", text));
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x0005EED8 File Offset: 0x0005D0D8
		private void ConfigureEndpoint()
		{
			this.Context.Properties.Set(ContextPropertySchema.RpcProxyServer, base.Definition.Endpoint);
			this.Context.Properties.Set(ContextPropertySchema.RpcServer, base.Definition.SecondaryEndpoint);
			this.Context.Properties.Set(ContextPropertySchema.WebProxyServer, "<none>");
			this.Context.Properties.Set(ContextPropertySchema.MapiHttpServer, base.Definition.Endpoint);
			this.Context.Properties.Set(ContextPropertySchema.MapiHttpPersonalizedServerName, base.Definition.SecondaryEndpoint);
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x0005EF80 File Offset: 0x0005D180
		private T GetAsEnum<T>(string extendedAttributeName, T defaultValue)
		{
			string value;
			if (!base.Definition.Attributes.TryGetValue(extendedAttributeName, out value))
			{
				return defaultValue;
			}
			return (T)((object)Enum.Parse(typeof(T), value));
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x0005EFBC File Offset: 0x0005D1BC
		protected string GetOspOutsideInChartUrl()
		{
			string text;
			if ((!Datacenter.IsMicrosoftHostedOnly(true) && !Datacenter.IsDatacenterDedicated(true)) || !base.Definition.Attributes.TryGetValue("SiteName", out text) || string.IsNullOrEmpty(text))
			{
				return null;
			}
			return Utils.GetOspOutsideInProbeResultLink("OutlookCTP Probe", string.Format("*.*.*.{0}", text));
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x0005F014 File Offset: 0x0005D214
		protected virtual void FillOutStateAttributes()
		{
			IPropertyBag properties = this.Context.Properties;
			this.Result.RespondingHttpServer = (properties.GetOrDefault(ContextPropertySchema.RespondingHttpServer) ?? properties.GetOrDefault(ContextPropertySchema.RpcProxyServer));
			this.Result.RespondingRpcProxyServer = (properties.GetOrDefault(ContextPropertySchema.RespondingRpcProxyServer) ?? "Unknown");
			this.Result.MonitoringAccount = base.Definition.Account;
			this.Result.OutlookSessionCookie = properties.GetOrDefault(ContextPropertySchema.OutlookSessionCookieValue);
			this.Result.UserLegacyDN = properties.GetOrDefault(ContextPropertySchema.UserLegacyDN);
			this.Result.RequestUrl = properties.GetOrDefault(ContextPropertySchema.RequestUrl);
			this.Result.AuthType = properties.Get(ContextPropertySchema.RpcProxyAuthenticationType).ToString();
			this.Result.TotalLatency = this.outlineLogger.TotalLatency;
			this.Result.ActivityContext = properties.GetOrDefault(ContextPropertySchema.ActivityContext);
			this.Result.FirstFailedTaskName = this.outlineLogger.FirstFailedTaskName;
			this.Result.ExecutionOutline = this.outlineLogger.GetExecutionOutline();
			this.Result.OspUrl = this.GetOspOutsideInChartUrl();
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x0005F154 File Offset: 0x0005D354
		private void TranslateTaskResults(Task parentTask)
		{
			try
			{
				this.FillOutStateAttributes();
				IPropertyBag properties = this.Context.Properties;
				Exception ex = null;
				if (this.Task.Result == TaskResult.Success && this.outlineLogger.TotalLatency > MapiProbe.UserPerceivedScenarioTimeout)
				{
					this.SetRootCause("HighLatency");
				}
				else if (this.Task.Result == TaskResult.Failed)
				{
					this.Result.VerboseLog = this.briefLogger.GetExecutionContextLog();
					this.Result.ErrorDetails = properties.GetOrDefault(ContextPropertySchema.ErrorDetails);
					if (!this.Context.Properties.TryGet(ContextPropertySchema.Exception, out ex))
					{
						ex = new Exception(this.Result.ErrorDetails ?? this.Result.VerboseLog);
					}
				}
				else
				{
					ex = null;
				}
				if (ex != null)
				{
					this.SetRootCause("UnknownIssue");
					this.ProcessTaskException(ex);
				}
			}
			finally
			{
				this.Result.OnBeforeSerialize();
			}
		}

		// Token: 0x04000A79 RID: 2681
		public const string Account = "Account";

		// Token: 0x04000A7A RID: 2682
		public const string Password = "Password";

		// Token: 0x04000A7B RID: 2683
		public const string Identity = "Identity";

		// Token: 0x04000A7C RID: 2684
		public const string Endpoint = "Endpoint";

		// Token: 0x04000A7D RID: 2685
		public const string SecondaryEndpoint = "SecondaryEndpoint";

		// Token: 0x04000A7E RID: 2686
		public const string ItemTargetExtension = "ItemTargetExtension";

		// Token: 0x04000A7F RID: 2687
		public const string TimeoutSeconds = "TimeoutSeconds";

		// Token: 0x04000A80 RID: 2688
		public const string AccountDisplayName = "AccountDisplayName";

		// Token: 0x04000A81 RID: 2689
		public const string ActAsLegacyDN = "AccountLegacyDN";

		// Token: 0x04000A82 RID: 2690
		public const string MailboxLegacyDN = "MailboxLegacyDN";

		// Token: 0x04000A83 RID: 2691
		public const string PersonalizedServerName = "PersonalizedServerName";

		// Token: 0x04000A84 RID: 2692
		public const string RpcProxyPort = "RpcProxyPort";

		// Token: 0x04000A85 RID: 2693
		public const string RpcProxyAuthenticationType = "RpcProxyAuthenticationType";

		// Token: 0x04000A86 RID: 2694
		public const string RpcAuthenticationType = "RpcAuthenticationType";

		// Token: 0x04000A87 RID: 2695
		public const string SiteName = "SiteName";

		// Token: 0x04000A88 RID: 2696
		protected const int HttpStatusCodeDatabaseMountedElsewhere = 555;

		// Token: 0x04000A89 RID: 2697
		protected const int AccountLockedOutOrTermsOfUseNotAccepted = 456;

		// Token: 0x04000A8A RID: 2698
		protected const int AccountPasswordExpired = 457;

		// Token: 0x04000A8B RID: 2699
		public static readonly TimeSpan UserPerceivedScenarioTimeout = TimeSpan.FromSeconds(30.0);

		// Token: 0x04000A8C RID: 2700
		private readonly MapiProbe.BriefLogger briefLogger = new MapiProbe.BriefLogger();

		// Token: 0x04000A8D RID: 2701
		private readonly MapiProbe.OutlineLogger outlineLogger = new MapiProbe.OutlineLogger();

		// Token: 0x04000A8E RID: 2702
		private readonly Dictionary<string, FailingComponent> RootCauseToComponentMapping = new Dictionary<string, FailingComponent>();

		// Token: 0x04000A8F RID: 2703
		private readonly MapiHttpVersion mapiHttpVerbsUpdateVersion = new MapiHttpVersion(15, 0, 732, 0);

		// Token: 0x04000A90 RID: 2704
		private static readonly string[] KnownStorePassiveMDBExceptions = new string[]
		{
			typeof(MapiExceptionIllegalCrossServerConnection).Name,
			typeof(WrongServerException).Name,
			ErrorCode.WrongServer.ToString()
		};

		// Token: 0x04000A91 RID: 2705
		private static readonly string[] KnownStoreMailboxExceptions = new string[]
		{
			typeof(MapiExceptionMailboxQuarantined).Name,
			typeof(MapiExceptionMdbOffline).Name,
			ErrorCode.MdbOffline.ToString()
		};

		// Token: 0x020001F5 RID: 501
		private class OutlineLogger : ILogger
		{
			// Token: 0x170002CA RID: 714
			// (get) Token: 0x06000E15 RID: 3605 RVA: 0x0005F308 File Offset: 0x0005D508
			public string FirstFailedTaskName
			{
				get
				{
					MapiProbe.OutlineLogger.TaskExecutionRecord taskExecutionRecord = this.executionOutline.FirstOrDefault((MapiProbe.OutlineLogger.TaskExecutionRecord record) => record.Result == TaskResult.Failed);
					if (taskExecutionRecord == null)
					{
						return null;
					}
					return taskExecutionRecord.Task.TaskTitle;
				}
			}

			// Token: 0x170002CB RID: 715
			// (get) Token: 0x06000E16 RID: 3606 RVA: 0x0005F361 File Offset: 0x0005D561
			public TimeSpan TotalLatency
			{
				get
				{
					return this.executionOutline.Aggregate(TimeSpan.Zero, (TimeSpan accumulator, MapiProbe.OutlineLogger.TaskExecutionRecord record) => accumulator + record.Latency);
				}
			}

			// Token: 0x06000E17 RID: 3607 RVA: 0x0005F40D File Offset: 0x0005D60D
			public string GetExecutionOutline()
			{
				return string.Concat(from record in this.executionOutline
				select string.Format("[{0}]{1} {2} {3}; ", new object[]
				{
					(int)record.Latency.TotalMilliseconds,
					record.StartTime.ToString(),
					(record.Result == TaskResult.Success) ? LocalizedString.Empty : "[FAILED!]",
					record.Task.TaskTitle
				}));
			}

			// Token: 0x06000E18 RID: 3608 RVA: 0x0005F43C File Offset: 0x0005D63C
			void ILogger.TaskStarted(ITaskDescriptor task)
			{
			}

			// Token: 0x06000E19 RID: 3609 RVA: 0x0005F440 File Offset: 0x0005D640
			void ILogger.TaskCompleted(ITaskDescriptor task, TaskResult result)
			{
				if (this.ShouldLogTask(task))
				{
					TimeSpan latency;
					task.Properties.TryGet(ContextPropertySchema.Latency, out latency);
					ExDateTime startTime;
					task.Properties.TryGet(ContextPropertySchema.TaskStarted, out startTime);
					this.executionOutline.Add(new MapiProbe.OutlineLogger.TaskExecutionRecord
					{
						Task = task,
						Result = result,
						Latency = latency,
						StartTime = startTime
					});
				}
			}

			// Token: 0x06000E1A RID: 3610 RVA: 0x0005F4AA File Offset: 0x0005D6AA
			protected bool ShouldLogTask(ITaskDescriptor task)
			{
				return task.DependentProperties.Contains(ContextPropertySchema.Latency);
			}

			// Token: 0x04000A96 RID: 2710
			private readonly List<MapiProbe.OutlineLogger.TaskExecutionRecord> executionOutline = new List<MapiProbe.OutlineLogger.TaskExecutionRecord>();

			// Token: 0x020001F6 RID: 502
			private class TaskExecutionRecord
			{
				// Token: 0x170002CC RID: 716
				// (get) Token: 0x06000E1F RID: 3615 RVA: 0x0005F4CF File Offset: 0x0005D6CF
				// (set) Token: 0x06000E20 RID: 3616 RVA: 0x0005F4D7 File Offset: 0x0005D6D7
				public ITaskDescriptor Task { get; set; }

				// Token: 0x170002CD RID: 717
				// (get) Token: 0x06000E21 RID: 3617 RVA: 0x0005F4E0 File Offset: 0x0005D6E0
				// (set) Token: 0x06000E22 RID: 3618 RVA: 0x0005F4E8 File Offset: 0x0005D6E8
				public TaskResult Result { get; set; }

				// Token: 0x170002CE RID: 718
				// (get) Token: 0x06000E23 RID: 3619 RVA: 0x0005F4F1 File Offset: 0x0005D6F1
				// (set) Token: 0x06000E24 RID: 3620 RVA: 0x0005F4F9 File Offset: 0x0005D6F9
				public TimeSpan Latency { get; set; }

				// Token: 0x170002CF RID: 719
				// (get) Token: 0x06000E25 RID: 3621 RVA: 0x0005F502 File Offset: 0x0005D702
				// (set) Token: 0x06000E26 RID: 3622 RVA: 0x0005F50A File Offset: 0x0005D70A
				public ExDateTime StartTime { get; set; }
			}
		}

		// Token: 0x020001F7 RID: 503
		private class BriefLogger : ScomAlertLogger
		{
			// Token: 0x06000E28 RID: 3624 RVA: 0x0005F51B File Offset: 0x0005D71B
			public string GetExecutionContextLog()
			{
				return this.taskLog.ToString();
			}

			// Token: 0x06000E29 RID: 3625 RVA: 0x0005F528 File Offset: 0x0005D728
			protected override void LogTaskCaption(ITaskDescriptor task)
			{
				base.LogHierarchicalOutput(task.TaskTitle);
			}

			// Token: 0x06000E2A RID: 3626 RVA: 0x0005F536 File Offset: 0x0005D736
			protected override void LogInputProperties(ITaskDescriptor task)
			{
			}

			// Token: 0x06000E2B RID: 3627 RVA: 0x0005F538 File Offset: 0x0005D738
			protected override void OnLogOutput(LocalizedString output)
			{
				this.taskLog.AppendLine(output);
				base.OnLogOutput(output);
			}

			// Token: 0x06000E2C RID: 3628 RVA: 0x0005F553 File Offset: 0x0005D753
			protected override bool ShouldLogTask(ITaskDescriptor task)
			{
				return task.TaskType != TaskType.Infrastructure;
			}

			// Token: 0x06000E2D RID: 3629 RVA: 0x0005F561 File Offset: 0x0005D761
			public BriefLogger() : base(null)
			{
			}

			// Token: 0x04000A9E RID: 2718
			private readonly StringBuilder taskLog = new StringBuilder();
		}
	}
}
