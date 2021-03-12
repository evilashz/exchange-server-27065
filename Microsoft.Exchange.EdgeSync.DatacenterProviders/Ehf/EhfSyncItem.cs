using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Text;
using Microsoft.Exchange.EdgeSync.Common;
using Microsoft.Exchange.EdgeSync.Datacenter;

namespace Microsoft.Exchange.EdgeSync.Ehf
{
	// Token: 0x02000018 RID: 24
	internal class EhfSyncItem
	{
		// Token: 0x060000D5 RID: 213 RVA: 0x00006D06 File Offset: 0x00004F06
		protected EhfSyncItem(ExSearchResultEntry entry, EdgeSyncDiag diagSession)
		{
			if (entry == null)
			{
				throw new ArgumentNullException("entry");
			}
			if (diagSession == null)
			{
				throw new ArgumentNullException("diagSession");
			}
			this.entry = entry;
			this.diagSession = diagSession;
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00006D38 File Offset: 0x00004F38
		public ExSearchResultEntry ADEntry
		{
			get
			{
				return this.entry;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00006D40 File Offset: 0x00004F40
		public string DistinguishedName
		{
			get
			{
				return this.entry.DistinguishedName;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00006D4D File Offset: 0x00004F4D
		public bool IsDeleted
		{
			get
			{
				return this.entry.IsDeleted;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00006D5A File Offset: 0x00004F5A
		protected EdgeSyncDiag DiagSession
		{
			get
			{
				return this.diagSession;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00006D62 File Offset: 0x00004F62
		private bool ContainsCurrentSyncErrors
		{
			get
			{
				return this.syncErrors != null && this.syncErrors.Count > 0;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00006D7C File Offset: 0x00004F7C
		private bool ContainsPreviousSyncErrors
		{
			get
			{
				return this.entry.GetAttribute("msExchEdgeSyncCookies") != null;
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00006D94 File Offset: 0x00004F94
		public void AddSyncError(string errorMessage)
		{
			if (this.syncErrors == null)
			{
				this.syncErrors = new List<string>();
			}
			this.syncErrors.Add(errorMessage);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00006DB8 File Offset: 0x00004FB8
		public void AddSyncError(string messageFormat, params object[] args)
		{
			string diagString = EdgeSyncDiag.GetDiagString(messageFormat, args);
			this.AddSyncError(diagString);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00006DD4 File Offset: 0x00004FD4
		public bool EventLogAndTryStoreSyncErrors(EhfADAdapter adapter)
		{
			this.EventLogSyncErrors();
			return this.entry.IsDeleted || this.TryStoreSyncErrors(adapter);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00006DF4 File Offset: 0x00004FF4
		protected static int GetFlagsValue(string flagsAttrName, ExSearchResultEntry resultEntry, EhfSyncItem syncItem)
		{
			DirectoryAttribute attribute = resultEntry.GetAttribute(flagsAttrName);
			if (attribute == null)
			{
				return 0;
			}
			string text = (string)attribute[0];
			if (string.IsNullOrEmpty(text))
			{
				return 0;
			}
			int result;
			if (!int.TryParse(text, out result))
			{
				syncItem.AddSyncError(syncItem.DiagSession.LogAndTraceError("Unable to parse flags value ({0}) of attribute {1} for AD object ({2}); using default value 0", new object[]
				{
					text,
					flagsAttrName,
					resultEntry.DistinguishedName
				}));
			}
			return result;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00006E5F File Offset: 0x0000505F
		protected Guid GetObjectGuid()
		{
			return this.entry.GetObjectGuid();
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00006E6C File Offset: 0x0000506C
		protected int GetFlagsValue(string flagsAttrName)
		{
			return EhfSyncItem.GetFlagsValue(flagsAttrName, this.entry, this);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00006E7C File Offset: 0x0000507C
		private void EventLogSyncErrors()
		{
			if (!this.ContainsCurrentSyncErrors)
			{
				return;
			}
			string text;
			if (this.syncErrors.Count == 1)
			{
				text = this.syncErrors[0];
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string value in this.syncErrors)
				{
					stringBuilder.AppendLine(value);
				}
				text = stringBuilder.ToString();
			}
			this.DiagSession.EventLog.LogEvent(EdgeSyncEventLogConstants.Tuple_EhfEntrySyncFailure, null, new object[]
			{
				this.DistinguishedName,
				text
			});
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00006F34 File Offset: 0x00005134
		private bool TryStoreSyncErrors(EhfADAdapter adapter)
		{
			object[] array = null;
			string text = null;
			if (this.ContainsCurrentSyncErrors)
			{
				array = new object[this.syncErrors.Count];
				for (int i = 0; i < this.syncErrors.Count; i++)
				{
					array[i] = this.syncErrors[i];
				}
				text = "save";
			}
			else if (this.ContainsPreviousSyncErrors)
			{
				text = "remove";
			}
			if (text == null)
			{
				return true;
			}
			Guid objectGuid = this.GetObjectGuid();
			try
			{
				adapter.SetAttributeValues(objectGuid, "msExchEdgeSyncCookies", array);
			}
			catch (ExDirectoryException ex)
			{
				if (ex.ResultCode == ResultCode.NoSuchObject)
				{
					this.DiagSession.LogAndTraceException(ex, "NoSuchObject error occurred while trying to {0} sync errors for AD object <{1}>:<{2}>; ignoring the error", new object[]
					{
						text,
						this.DistinguishedName,
						objectGuid
					});
					return true;
				}
				this.DiagSession.LogAndTraceException(ex, "Exception occurred while trying to {0} sync errors for AD object <{1}>:<{2}>", new object[]
				{
					text,
					this.DistinguishedName,
					objectGuid
				});
				this.DiagSession.EventLog.LogEvent(EdgeSyncEventLogConstants.Tuple_EhfFailedUpdateSyncErrors, this.DistinguishedName, new object[]
				{
					this.DistinguishedName,
					text,
					ex
				});
				return false;
			}
			this.DiagSession.Tracer.TraceDebug<string, string, Guid>((long)this.DiagSession.GetHashCode(), "Successfully {0}d sync errors for AD object <{1}>:<{2}>", text, this.DistinguishedName, objectGuid);
			return true;
		}

		// Token: 0x04000051 RID: 81
		private ExSearchResultEntry entry;

		// Token: 0x04000052 RID: 82
		private EdgeSyncDiag diagSession;

		// Token: 0x04000053 RID: 83
		private List<string> syncErrors;
	}
}
