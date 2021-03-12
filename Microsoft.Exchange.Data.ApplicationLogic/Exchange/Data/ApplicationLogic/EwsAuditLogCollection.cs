using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage.Auditing;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.Data.ApplicationLogic
{
	// Token: 0x020000A2 RID: 162
	internal class EwsAuditLogCollection : IAuditLogCollection
	{
		// Token: 0x060006F7 RID: 1783 RVA: 0x0001B0DA File Offset: 0x000192DA
		public EwsAuditLogCollection(EwsAuditClient ewsClient, FolderIdType auditRootFolderId)
		{
			this.ewsClient = ewsClient;
			this.auditRootFolderId = auditRootFolderId;
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x0001B0F0 File Offset: 0x000192F0
		public Trace Tracer
		{
			get
			{
				return this.ewsClient.Tracer;
			}
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0001B344 File Offset: 0x00019544
		public IEnumerable<IAuditLog> GetAuditLogs()
		{
			BaseFolderType[] subfolders = this.ewsClient.GetSubFolders(this.auditRootFolderId, null);
			foreach (BaseFolderType subfolder in subfolders)
			{
				DateTime logRangeStart;
				DateTime logRangeEnd;
				bool validLogSubfolder = AuditLogCollection.TryParseLogRange(subfolder.DisplayName, out logRangeStart, out logRangeEnd);
				if (validLogSubfolder && subfolder.TotalCount > 0)
				{
					yield return new EwsAuditLog(this.ewsClient, subfolder.FolderId, logRangeStart, logRangeEnd);
				}
			}
			yield return new EwsAuditLog(this.ewsClient, this.auditRootFolderId, DateTime.MinValue, DateTime.MaxValue);
			yield break;
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0001B364 File Offset: 0x00019564
		public bool FindLog(DateTime timestamp, bool createIfNotExists, out IAuditLog auditLog)
		{
			auditLog = null;
			DateTime dateTime;
			DateTime dateTime2;
			string logFolderNameAndRange = AuditLogCollection.GetLogFolderNameAndRange(timestamp, out dateTime, out dateTime2);
			FolderIdType folderIdType = null;
			if (!this.ewsClient.FindFolder(logFolderNameAndRange, this.auditRootFolderId, out folderIdType))
			{
				if (!createIfNotExists)
				{
					if (this.IsTraceEnabled(TraceType.DebugTrace))
					{
						this.Tracer.TraceDebug<DateTime>((long)this.GetHashCode(), "No matching log subfolder found. Lookup time={0}", timestamp);
					}
					return false;
				}
				bool flag = this.ewsClient.CreateFolder(logFolderNameAndRange, this.auditRootFolderId, out folderIdType);
				if (!flag)
				{
					flag = this.ewsClient.FindFolder(logFolderNameAndRange, this.auditRootFolderId, out folderIdType);
				}
				if (!flag && this.IsTraceEnabled(TraceType.DebugTrace))
				{
					this.Tracer.TraceDebug<DateTime, DateTime>((long)this.GetHashCode(), "Failed to create audit log folder for log range [{0}, {1})", dateTime, dateTime2);
				}
			}
			if (folderIdType != null)
			{
				auditLog = new EwsAuditLog(this.ewsClient, folderIdType, dateTime, dateTime2);
			}
			return folderIdType != null;
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x0001B431 File Offset: 0x00019631
		private bool IsTraceEnabled(TraceType traceType)
		{
			return this.Tracer != null && this.Tracer.IsTraceEnabled(TraceType.DebugTrace);
		}

		// Token: 0x0400031F RID: 799
		private EwsAuditClient ewsClient;

		// Token: 0x04000320 RID: 800
		private FolderIdType auditRootFolderId;
	}
}
