using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Common.IL;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;
using Microsoft.Exchange.InfoWorker.Common;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000060 RID: 96
	internal sealed class EnforcerManager
	{
		// Token: 0x0600035F RID: 863 RVA: 0x00017532 File Offset: 0x00015732
		internal EnforcerManager(DatabaseInfo databaseInfo, ElcAuditLog elcAuditLog, ElcFolderSubAssistant assistant)
		{
			this.databaseInfo = databaseInfo;
			this.elcAuditLog = elcAuditLog;
			this.elcAssistant = assistant;
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0001754F File Offset: 0x0001574F
		public override string ToString()
		{
			if (this.toString == null)
			{
				this.toString = "Enforcer manager for " + this.databaseInfo.ToString();
			}
			return this.toString;
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0001757A File Offset: 0x0001577A
		internal void OnWindowBegin()
		{
			EnforcerManager.Tracer.TraceDebug<EnforcerManager>((long)this.GetHashCode(), "{0}: OnWindowBegin called.", this);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00017593 File Offset: 0x00015793
		internal void OnWindowEnd()
		{
			EnforcerManager.Tracer.TraceDebug<EnforcerManager>((long)this.GetHashCode(), "{0}: OnWindowEnd called.", this);
		}

		// Token: 0x06000363 RID: 867 RVA: 0x000179D8 File Offset: 0x00015BD8
		internal void Invoke(MailboxSession mailboxSession, ElcUserFolderInformation userELCInfo)
		{
			EnforcerManager.<>c__DisplayClass4 CS$<>8__locals1 = new EnforcerManager.<>c__DisplayClass4();
			CS$<>8__locals1.mailboxSession = mailboxSession;
			CS$<>8__locals1.<>4__this = this;
			EnforcerManager.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: Invoke called.", new object[]
			{
				TraceContext.Get()
			});
			EnforcerManager.TracerPfd.TracePfd<int, object>((long)this.GetHashCode(), "PFD IWE {0} {1}: Invoke called.", 32023, TraceContext.Get());
			EnforcerManager.<>c__DisplayClass6 CS$<>8__locals2 = new EnforcerManager.<>c__DisplayClass6();
			CS$<>8__locals2.mailboxData = new MailboxDataForFolders(userELCInfo, this.elcAuditLog);
			try
			{
				EnforcerManager.<>c__DisplayClass8 CS$<>8__locals3 = new EnforcerManager.<>c__DisplayClass8();
				CS$<>8__locals3.CS$<>8__locals7 = CS$<>8__locals2;
				CS$<>8__locals3.CS$<>8__locals5 = CS$<>8__locals1;
				CS$<>8__locals3.enforcerArray = new EnforcerBase[]
				{
					new AutoCopyEnforcer(CS$<>8__locals2.mailboxData, this.databaseInfo, this.elcAssistant),
					new ExpirationEnforcer(CS$<>8__locals2.mailboxData, this.elcAssistant)
				};
				CS$<>8__locals3.mailboxOwner = CS$<>8__locals1.mailboxSession.MailboxOwner;
				CS$<>8__locals3.folderForFilter = null;
				ILUtil.DoTryFilterCatch(new TryDelegate(CS$<>8__locals3, (UIntPtr)ldftn(<Invoke>b__0)), new FilterDelegate(CS$<>8__locals3, (UIntPtr)ldftn(<Invoke>b__1)), new CatchDelegate(null, (UIntPtr)ldftn(<Invoke>b__2)));
			}
			finally
			{
				if (CS$<>8__locals2.mailboxData != null)
				{
					((IDisposable)CS$<>8__locals2.mailboxData).Dispose();
				}
			}
		}

		// Token: 0x040002D0 RID: 720
		private static readonly Trace Tracer = ExTraceGlobals.CommonEnforcerOperationsTracer;

		// Token: 0x040002D1 RID: 721
		private static readonly Trace TracerPfd = ExTraceGlobals.PFDTracer;

		// Token: 0x040002D2 RID: 722
		private DatabaseInfo databaseInfo;

		// Token: 0x040002D3 RID: 723
		private ElcAuditLog elcAuditLog;

		// Token: 0x040002D4 RID: 724
		private string toString;

		// Token: 0x040002D5 RID: 725
		private ElcFolderSubAssistant elcAssistant;
	}
}
