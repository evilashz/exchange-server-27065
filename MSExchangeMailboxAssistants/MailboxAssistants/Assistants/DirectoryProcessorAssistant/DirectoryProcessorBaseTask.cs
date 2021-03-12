using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.DirectoryProcessorAssistant
{
	// Token: 0x02000196 RID: 406
	internal abstract class DirectoryProcessorBaseTask
	{
		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000FEA RID: 4074 RVA: 0x0005D83B File Offset: 0x0005BA3B
		// (set) Token: 0x06000FEB RID: 4075 RVA: 0x0005D843 File Offset: 0x0005BA43
		private protected Logger Logger { protected get; private set; }

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000FEC RID: 4076 RVA: 0x0005D84C File Offset: 0x0005BA4C
		protected string TenantId
		{
			get
			{
				return this.RunData.TenantId;
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000FED RID: 4077 RVA: 0x0005D859 File Offset: 0x0005BA59
		protected Guid RunId
		{
			get
			{
				return this.RunData.RunId;
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06000FEE RID: 4078 RVA: 0x0005D866 File Offset: 0x0005BA66
		protected Guid MailboxGuid
		{
			get
			{
				return this.RunData.MailboxGuid;
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06000FEF RID: 4079 RVA: 0x0005D873 File Offset: 0x0005BA73
		protected Guid DatabaseGuid
		{
			get
			{
				return this.RunData.DatabaseGuid;
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06000FF0 RID: 4080 RVA: 0x0005D880 File Offset: 0x0005BA80
		protected OrganizationId OrgId
		{
			get
			{
				return this.RunData.OrgId;
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06000FF1 RID: 4081 RVA: 0x0005D88D File Offset: 0x0005BA8D
		// (set) Token: 0x06000FF2 RID: 4082 RVA: 0x0005D895 File Offset: 0x0005BA95
		internal RunData RunData { get; private set; }

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06000FF3 RID: 4083 RVA: 0x0005D89E File Offset: 0x0005BA9E
		internal virtual string ClassName
		{
			get
			{
				if (this.className == null)
				{
					this.className = base.GetType().Name;
				}
				return this.className;
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06000FF4 RID: 4084 RVA: 0x0005D8BF File Offset: 0x0005BABF
		public DateTime RunStartTime
		{
			get
			{
				return this.RunData.RunStartTime;
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000FF5 RID: 4085
		protected abstract Trace Trace { get; }

		// Token: 0x06000FF6 RID: 4086 RVA: 0x0005D8CC File Offset: 0x0005BACC
		protected Logger CreateLogger(Trace trace)
		{
			return new Logger(this.RunData, trace, this.ClassName);
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x0005D8E0 File Offset: 0x0005BAE0
		public DirectoryProcessorBaseTask(RunData runData)
		{
			ValidateArgument.NotNull(runData, "runData");
			this.RunData = runData;
			this.Logger = this.CreateLogger(this.Trace);
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000FF8 RID: 4088 RVA: 0x0005D90C File Offset: 0x0005BB0C
		public virtual bool ShouldDeferFinalize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x0005D90F File Offset: 0x0005BB0F
		public virtual void Initialize(RecipientType recipientType)
		{
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x0005D911 File Offset: 0x0005BB11
		public virtual bool ShouldRun(RecipientType recipientType)
		{
			return true;
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x0005D914 File Offset: 0x0005BB14
		public virtual bool ShouldWatson(Exception e)
		{
			return true;
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x0005D917 File Offset: 0x0005BB17
		public virtual void FinalizeMe(DirectoryProcessorBaseTaskContext taskContext)
		{
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x0005D919 File Offset: 0x0005BB19
		public DirectoryProcessorBaseTaskContext DoChunk(DirectoryProcessorBaseTaskContext context, RecipientType recipientType)
		{
			this.Logger.SetMetadataValues(this, recipientType, string.Empty);
			return this.DoChunkWork(context, recipientType);
		}

		// Token: 0x06000FFE RID: 4094
		protected abstract DirectoryProcessorBaseTaskContext DoChunkWork(DirectoryProcessorBaseTaskContext context, RecipientType recipientType);

		// Token: 0x04000A27 RID: 2599
		private string className;
	}
}
