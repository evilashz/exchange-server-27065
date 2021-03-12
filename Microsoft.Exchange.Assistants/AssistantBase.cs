using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000014 RID: 20
	internal abstract class AssistantBase
	{
		// Token: 0x06000058 RID: 88 RVA: 0x0000400C File Offset: 0x0000220C
		public AssistantBase(DatabaseInfo databaseInfo, LocalizedString name, string nonLocalizedName)
		{
			if (databaseInfo == null)
			{
				throw new ArgumentNullException("databaseInfo");
			}
			this.databaseInfo = databaseInfo;
			this.Name = name;
			this.NonLocalizedName = nonLocalizedName;
			AssistantBase.Tracer.TraceDebug<AssistantBase>((long)this.GetHashCode(), "{0}: created", this);
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00004059 File Offset: 0x00002259
		public DatabaseInfo DatabaseInfo
		{
			get
			{
				return this.databaseInfo;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00004061 File Offset: 0x00002261
		public bool Shutdown
		{
			get
			{
				return this.shutdown;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00004069 File Offset: 0x00002269
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00004071 File Offset: 0x00002271
		public LocalizedString Name { get; private set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600005D RID: 93 RVA: 0x0000407A File Offset: 0x0000227A
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00004082 File Offset: 0x00002282
		public string NonLocalizedName { get; private set; }

		// Token: 0x0600005F RID: 95 RVA: 0x0000408C File Offset: 0x0000228C
		public void OnShutdown()
		{
			AssistantBase.Tracer.TraceDebug<AssistantBase>((long)this.GetHashCode(), "{0}: OnShutdown started", this);
			this.shutdown = true;
			this.OnShutdownInternal();
			AssistantBase.Tracer.TraceDebug<AssistantBase>((long)this.GetHashCode(), "{0}: OnShutdown completed", this);
			AssistantBase.TracerPfd.TracePfd<int, AssistantBase>((long)this.GetHashCode(), "PFD IWS {0} {1}: Shutdown", 23319, this);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000040F0 File Offset: 0x000022F0
		public override string ToString()
		{
			if (this.toString == null)
			{
				this.toString = this.NonLocalizedName + " for " + this.DatabaseInfo.ToString();
			}
			return this.toString;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00004121 File Offset: 0x00002321
		protected virtual void OnShutdownInternal()
		{
		}

		// Token: 0x040000A3 RID: 163
		private static readonly Trace Tracer = ExTraceGlobals.AssistantBaseTracer;

		// Token: 0x040000A4 RID: 164
		private static readonly Trace TracerPfd = ExTraceGlobals.PFDTracer;

		// Token: 0x040000A5 RID: 165
		private DatabaseInfo databaseInfo;

		// Token: 0x040000A6 RID: 166
		private bool shutdown;

		// Token: 0x040000A7 RID: 167
		private string toString;
	}
}
