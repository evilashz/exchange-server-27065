using System;
using System.Net;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Net.AAD;
using Microsoft.Exchange.Security.OAuth;
using Microsoft.Exchange.UnifiedGroups;
using Microsoft.SharePoint.Client;

namespace Microsoft.Exchange.FederatedDirectory
{
	// Token: 0x02000005 RID: 5
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class UnifiedGroupsTask
	{
		// Token: 0x06000010 RID: 16 RVA: 0x00002379 File Offset: 0x00000579
		public UnifiedGroupsTask(ADUser accessingUser, IRecipientSession adSession) : this(accessingUser, adSession, UnifiedGroupsTask.GetActivityId())
		{
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002388 File Offset: 0x00000588
		public UnifiedGroupsTask(ADUser accessingUser, IRecipientSession adSession, Guid activityId)
		{
			this.AccessingUser = accessingUser;
			this.ADSession = adSession;
			this.AADClient = AADClientFactory.Create(accessingUser);
			this.ActivityId = activityId;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000023B1 File Offset: 0x000005B1
		// (set) Token: 0x06000013 RID: 19 RVA: 0x000023B9 File Offset: 0x000005B9
		public UnifiedGroupsTask.UnifiedGroupsAction ErrorAction { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000023C2 File Offset: 0x000005C2
		// (set) Token: 0x06000015 RID: 21 RVA: 0x000023CA File Offset: 0x000005CA
		public Exception ErrorException { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000023D3 File Offset: 0x000005D3
		// (set) Token: 0x06000017 RID: 23 RVA: 0x000023DB File Offset: 0x000005DB
		public string ErrorCode { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000023E4 File Offset: 0x000005E4
		// (set) Token: 0x06000019 RID: 25 RVA: 0x000023EC File Offset: 0x000005EC
		private protected ADUser AccessingUser { protected get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000023F5 File Offset: 0x000005F5
		// (set) Token: 0x0600001B RID: 27 RVA: 0x000023FD File Offset: 0x000005FD
		private protected IRecipientSession ADSession { protected get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002406 File Offset: 0x00000606
		protected ICredentials ActAsUserCredentials
		{
			get
			{
				if (this.actAsUserCredentials == null)
				{
					this.actAsUserCredentials = OAuthCredentials.GetOAuthCredentialsForAppActAsToken(this.AccessingUser.OrganizationId, this.AccessingUser, null);
				}
				return this.actAsUserCredentials;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002433 File Offset: 0x00000633
		// (set) Token: 0x0600001E RID: 30 RVA: 0x0000243B File Offset: 0x0000063B
		private protected AADClient AADClient { protected get; private set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002444 File Offset: 0x00000644
		protected bool IsAADEnabled
		{
			get
			{
				return this.AADClient != null;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002452 File Offset: 0x00000652
		protected bool IsSharePointEnabled
		{
			get
			{
				return this.AADClient != null && SharePointUrl.GetRootSiteUrl(this.AccessingUser.OrganizationId) != null;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002474 File Offset: 0x00000674
		// (set) Token: 0x06000022 RID: 34 RVA: 0x0000247C File Offset: 0x0000067C
		protected UnifiedGroupsTask.UnifiedGroupsAction CurrentAction { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002485 File Offset: 0x00000685
		// (set) Token: 0x06000024 RID: 36 RVA: 0x0000248D File Offset: 0x0000068D
		private protected Guid ActivityId { protected get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000025 RID: 37
		protected abstract string TaskName { get; }

		// Token: 0x06000026 RID: 38 RVA: 0x00002496 File Offset: 0x00000696
		public bool Run()
		{
			return this.RunWithLogging();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000256C File Offset: 0x0000076C
		protected static bool QueueTask(UnifiedGroupsTask task)
		{
			return ThreadPool.QueueUserWorkItem(delegate(object state)
			{
				try
				{
					GrayException.MapAndReportGrayExceptions(delegate()
					{
						task.RunWithLogging();
					});
				}
				catch (GrayException arg)
				{
					UnifiedGroupsTask.Tracer.TraceError<Guid, GrayException>(0L, "ActivityId: {0}. GrayException: {1}", task.ActivityId, arg);
					FederatedDirectoryLogger.AppendToLog(new SchemaBasedLogEvent<FederatedDirectoryLogSchema.TraceTag>
					{
						{
							FederatedDirectoryLogSchema.TraceTag.TaskName,
							task.TaskName
						},
						{
							FederatedDirectoryLogSchema.TraceTag.ActivityId,
							task.ActivityId
						},
						{
							FederatedDirectoryLogSchema.TraceTag.CurrentAction,
							task.CurrentAction
						},
						{
							FederatedDirectoryLogSchema.TraceTag.Message,
							"GrayException: " + arg
						}
					});
				}
			});
		}

		// Token: 0x06000028 RID: 40
		protected abstract void RunInternal();

		// Token: 0x06000029 RID: 41 RVA: 0x00002598 File Offset: 0x00000798
		private static Guid GetActivityId()
		{
			IActivityScope currentActivityScope = ActivityContext.GetCurrentActivityScope();
			if (currentActivityScope == null)
			{
				return Guid.NewGuid();
			}
			return currentActivityScope.ActivityId;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000025BC File Offset: 0x000007BC
		private bool RunWithLogging()
		{
			Exception ex = null;
			try
			{
				this.CurrentAction = UnifiedGroupsTask.UnifiedGroupsAction.None;
				this.RunInternal();
			}
			catch (AADDataException ex2)
			{
				this.ErrorCode = ex2.Code.ToString();
				ex = ex2;
			}
			catch (LocalizedException ex3)
			{
				ex = ex3;
			}
			catch (WebException ex4)
			{
				ex = ex4;
			}
			catch (ClientRequestException ex5)
			{
				ex = ex5;
			}
			catch (ServerException ex6)
			{
				ex = ex6;
			}
			if (ex != null)
			{
				UnifiedGroupsTask.Tracer.TraceError<UnifiedGroupsTask.UnifiedGroupsAction, Exception>((long)this.GetHashCode(), "UnifiedGroupsTask.RunWithLogging: RunInternal threw an exception. CurrentAction: {0}, exception: {1}", this.CurrentAction, ex);
				FederatedDirectoryLogger.AppendToLog(new SchemaBasedLogEvent<FederatedDirectoryLogSchema.ExceptionTag>
				{
					{
						FederatedDirectoryLogSchema.ExceptionTag.TaskName,
						this.TaskName
					},
					{
						FederatedDirectoryLogSchema.ExceptionTag.ActivityId,
						this.ActivityId
					},
					{
						FederatedDirectoryLogSchema.ExceptionTag.ExceptionType,
						ex.GetType()
					},
					{
						FederatedDirectoryLogSchema.ExceptionTag.ExceptionDetail,
						ex
					},
					{
						FederatedDirectoryLogSchema.ExceptionTag.CurrentAction,
						this.CurrentAction
					},
					{
						FederatedDirectoryLogSchema.ExceptionTag.Message,
						"RunInternal threw an exception"
					}
				});
				this.ErrorAction = this.CurrentAction;
				this.ErrorException = ex;
				return false;
			}
			return true;
		}

		// Token: 0x04000011 RID: 17
		internal static readonly Trace Tracer = ExTraceGlobals.ModernGroupsTracer;

		// Token: 0x04000012 RID: 18
		private ICredentials actAsUserCredentials;

		// Token: 0x02000006 RID: 6
		public enum UnifiedGroupsAction
		{
			// Token: 0x0400001C RID: 28
			None,
			// Token: 0x0400001D RID: 29
			AADCreate,
			// Token: 0x0400001E RID: 30
			AADAddOwnerAsMember,
			// Token: 0x0400001F RID: 31
			AADCompleteCallback,
			// Token: 0x04000020 RID: 32
			AADPostCreate,
			// Token: 0x04000021 RID: 33
			AADUpdate,
			// Token: 0x04000022 RID: 34
			AADDelete,
			// Token: 0x04000023 RID: 35
			ExchangeCreate,
			// Token: 0x04000024 RID: 36
			ExchangeUpdate,
			// Token: 0x04000025 RID: 37
			ExchangeDelete,
			// Token: 0x04000026 RID: 38
			ResolveExternalIdentities,
			// Token: 0x04000027 RID: 39
			SharePointCreate,
			// Token: 0x04000028 RID: 40
			SharePointSetMailboxUrls,
			// Token: 0x04000029 RID: 41
			SharePointUpdate,
			// Token: 0x0400002A RID: 42
			SharePointDelete,
			// Token: 0x0400002B RID: 43
			Completed
		}
	}
}
