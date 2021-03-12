using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x02000005 RID: 5
	internal sealed class PhysicalAccessPerformanceCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x06000025 RID: 37 RVA: 0x00009A2C File Offset: 0x00007C2C
		internal PhysicalAccessPerformanceCountersInstance(string instanceName, PhysicalAccessPerformanceCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchangeIS Physical Access")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.NumberOfQueriesPerSec = new ExPerformanceCounter(base.CategoryName, "Number of Queries per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.NumberOfQueriesPerSec, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfQueriesPerSec);
				this.NumberOfInsertsPerSec = new ExPerformanceCounter(base.CategoryName, "Number of Inserts per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.NumberOfInsertsPerSec, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfInsertsPerSec);
				this.NumberOfUpdatesPerSec = new ExPerformanceCounter(base.CategoryName, "Number of Updates per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.NumberOfUpdatesPerSec, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfUpdatesPerSec);
				this.NumberOfDeletesPerSec = new ExPerformanceCounter(base.CategoryName, "Number of Deletes per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.NumberOfDeletesPerSec, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfDeletesPerSec);
				this.NumberOfOthersPerSec = new ExPerformanceCounter(base.CategoryName, "Number of Others per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.NumberOfOthersPerSec, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfOthersPerSec);
				this.OffPageBlobHitsPerSec = new ExPerformanceCounter(base.CategoryName, "Number of off-page blob hits per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.OffPageBlobHitsPerSec, new ExPerformanceCounter[0]);
				list.Add(this.OffPageBlobHitsPerSec);
				this.OtherRowsReadRate = new ExPerformanceCounter(base.CategoryName, "Other - Rows read per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.OtherRowsReadRate, new ExPerformanceCounter[0]);
				list.Add(this.OtherRowsReadRate);
				this.OtherRowsSeekRate = new ExPerformanceCounter(base.CategoryName, "Other - Seeks per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.OtherRowsSeekRate, new ExPerformanceCounter[0]);
				list.Add(this.OtherRowsSeekRate);
				this.OtherRowsAcceptRate = new ExPerformanceCounter(base.CategoryName, "Other - Rows accepted per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.OtherRowsAcceptRate, new ExPerformanceCounter[0]);
				list.Add(this.OtherRowsAcceptRate);
				this.OtherRowsWriteRate = new ExPerformanceCounter(base.CategoryName, "Other - Rows written per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.OtherRowsWriteRate, new ExPerformanceCounter[0]);
				list.Add(this.OtherRowsWriteRate);
				this.OtherBytesReadRate = new ExPerformanceCounter(base.CategoryName, "Other - Bytes read per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.OtherBytesReadRate, new ExPerformanceCounter[0]);
				list.Add(this.OtherBytesReadRate);
				this.OtherBytesWriteRate = new ExPerformanceCounter(base.CategoryName, "Other - Bytes written per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.OtherBytesWriteRate, new ExPerformanceCounter[0]);
				list.Add(this.OtherBytesWriteRate);
				this.TableFunctionRowsReadRate = new ExPerformanceCounter(base.CategoryName, "TableFunction - Rows read per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TableFunctionRowsReadRate, new ExPerformanceCounter[0]);
				list.Add(this.TableFunctionRowsReadRate);
				this.TableFunctionRowsAcceptRate = new ExPerformanceCounter(base.CategoryName, "TableFunction - Rows accepted per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TableFunctionRowsAcceptRate, new ExPerformanceCounter[0]);
				list.Add(this.TableFunctionRowsAcceptRate);
				this.TableFunctionBytesReadRate = new ExPerformanceCounter(base.CategoryName, "TableFunction - Bytes read per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TableFunctionBytesReadRate, new ExPerformanceCounter[0]);
				list.Add(this.TableFunctionBytesReadRate);
				this.TempRowsReadRate = new ExPerformanceCounter(base.CategoryName, "Temp - Rows read per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TempRowsReadRate, new ExPerformanceCounter[0]);
				list.Add(this.TempRowsReadRate);
				this.TempRowsSeekRate = new ExPerformanceCounter(base.CategoryName, "Temp - Seeks per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TempRowsSeekRate, new ExPerformanceCounter[0]);
				list.Add(this.TempRowsSeekRate);
				this.TempRowsAcceptRate = new ExPerformanceCounter(base.CategoryName, "Temp - Rows accepted per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TempRowsAcceptRate, new ExPerformanceCounter[0]);
				list.Add(this.TempRowsAcceptRate);
				this.TempRowsWriteRate = new ExPerformanceCounter(base.CategoryName, "Temp - Rows written per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TempRowsWriteRate, new ExPerformanceCounter[0]);
				list.Add(this.TempRowsWriteRate);
				this.TempBytesReadRate = new ExPerformanceCounter(base.CategoryName, "Temp - Bytes read per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TempBytesReadRate, new ExPerformanceCounter[0]);
				list.Add(this.TempBytesReadRate);
				this.TempBytesWriteRate = new ExPerformanceCounter(base.CategoryName, "Temp - Bytes written per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TempBytesWriteRate, new ExPerformanceCounter[0]);
				list.Add(this.TempBytesWriteRate);
				this.LazyIndexRowsReadRate = new ExPerformanceCounter(base.CategoryName, "LazyIndex - Rows read per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.LazyIndexRowsReadRate, new ExPerformanceCounter[0]);
				list.Add(this.LazyIndexRowsReadRate);
				this.LazyIndexRowsSeekRate = new ExPerformanceCounter(base.CategoryName, "LazyIndex - Seeks per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.LazyIndexRowsSeekRate, new ExPerformanceCounter[0]);
				list.Add(this.LazyIndexRowsSeekRate);
				this.LazyIndexRowsAcceptRate = new ExPerformanceCounter(base.CategoryName, "LazyIndex - Rows accepted per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.LazyIndexRowsAcceptRate, new ExPerformanceCounter[0]);
				list.Add(this.LazyIndexRowsAcceptRate);
				this.LazyIndexRowsWriteRate = new ExPerformanceCounter(base.CategoryName, "LazyIndex - Rows written per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.LazyIndexRowsWriteRate, new ExPerformanceCounter[0]);
				list.Add(this.LazyIndexRowsWriteRate);
				this.LazyIndexBytesReadRate = new ExPerformanceCounter(base.CategoryName, "LazyIndex - Bytes read per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.LazyIndexBytesReadRate, new ExPerformanceCounter[0]);
				list.Add(this.LazyIndexBytesReadRate);
				this.LazyIndexBytesWriteRate = new ExPerformanceCounter(base.CategoryName, "LazyIndex - Bytes written per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.LazyIndexBytesWriteRate, new ExPerformanceCounter[0]);
				list.Add(this.LazyIndexBytesWriteRate);
				this.FolderRowsReadRate = new ExPerformanceCounter(base.CategoryName, "Folder - Rows read per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.FolderRowsReadRate, new ExPerformanceCounter[0]);
				list.Add(this.FolderRowsReadRate);
				this.FolderRowsSeekRate = new ExPerformanceCounter(base.CategoryName, "Folder - Seeks per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.FolderRowsSeekRate, new ExPerformanceCounter[0]);
				list.Add(this.FolderRowsSeekRate);
				this.FolderRowsAcceptRate = new ExPerformanceCounter(base.CategoryName, "Folder - Rows accepted per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.FolderRowsAcceptRate, new ExPerformanceCounter[0]);
				list.Add(this.FolderRowsAcceptRate);
				this.FolderRowsWriteRate = new ExPerformanceCounter(base.CategoryName, "Folder - Rows written per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.FolderRowsWriteRate, new ExPerformanceCounter[0]);
				list.Add(this.FolderRowsWriteRate);
				this.FolderBytesReadRate = new ExPerformanceCounter(base.CategoryName, "Folder - Bytes read per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.FolderBytesReadRate, new ExPerformanceCounter[0]);
				list.Add(this.FolderBytesReadRate);
				this.FolderBytesWriteRate = new ExPerformanceCounter(base.CategoryName, "Folder - Bytes written per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.FolderBytesWriteRate, new ExPerformanceCounter[0]);
				list.Add(this.FolderBytesWriteRate);
				this.MessageRowsReadRate = new ExPerformanceCounter(base.CategoryName, "Message - Rows read per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MessageRowsReadRate, new ExPerformanceCounter[0]);
				list.Add(this.MessageRowsReadRate);
				this.MessageRowsSeekRate = new ExPerformanceCounter(base.CategoryName, "Message - Seeks per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MessageRowsSeekRate, new ExPerformanceCounter[0]);
				list.Add(this.MessageRowsSeekRate);
				this.MessageRowsAcceptRate = new ExPerformanceCounter(base.CategoryName, "Message - Rows accepted per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MessageRowsAcceptRate, new ExPerformanceCounter[0]);
				list.Add(this.MessageRowsAcceptRate);
				this.MessageRowsWriteRate = new ExPerformanceCounter(base.CategoryName, "Message - Rows written per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MessageRowsWriteRate, new ExPerformanceCounter[0]);
				list.Add(this.MessageRowsWriteRate);
				this.MessageBytesReadRate = new ExPerformanceCounter(base.CategoryName, "Message - Bytes read per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MessageBytesReadRate, new ExPerformanceCounter[0]);
				list.Add(this.MessageBytesReadRate);
				this.MessageBytesWriteRate = new ExPerformanceCounter(base.CategoryName, "Message - Bytes written per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MessageBytesWriteRate, new ExPerformanceCounter[0]);
				list.Add(this.MessageBytesWriteRate);
				this.AttachmentRowsReadRate = new ExPerformanceCounter(base.CategoryName, "Attachment - Rows read per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AttachmentRowsReadRate, new ExPerformanceCounter[0]);
				list.Add(this.AttachmentRowsReadRate);
				this.AttachmentRowsSeekRate = new ExPerformanceCounter(base.CategoryName, "Attachment - Seeks per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AttachmentRowsSeekRate, new ExPerformanceCounter[0]);
				list.Add(this.AttachmentRowsSeekRate);
				this.AttachmentRowsAcceptRate = new ExPerformanceCounter(base.CategoryName, "Attachment - Rows accepted per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AttachmentRowsAcceptRate, new ExPerformanceCounter[0]);
				list.Add(this.AttachmentRowsAcceptRate);
				this.AttachmentRowsWriteRate = new ExPerformanceCounter(base.CategoryName, "Attachment - Rows written per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AttachmentRowsWriteRate, new ExPerformanceCounter[0]);
				list.Add(this.AttachmentRowsWriteRate);
				this.AttachmentBytesReadRate = new ExPerformanceCounter(base.CategoryName, "Attachment - Bytes read per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AttachmentBytesReadRate, new ExPerformanceCounter[0]);
				list.Add(this.AttachmentBytesReadRate);
				this.AttachmentBytesWriteRate = new ExPerformanceCounter(base.CategoryName, "Attachment - Bytes written per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AttachmentBytesWriteRate, new ExPerformanceCounter[0]);
				list.Add(this.AttachmentBytesWriteRate);
				this.PseudoIndexMaintenanceRowsReadRate = new ExPerformanceCounter(base.CategoryName, "PseudoIndexMaintenance - Rows read per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PseudoIndexMaintenanceRowsReadRate, new ExPerformanceCounter[0]);
				list.Add(this.PseudoIndexMaintenanceRowsReadRate);
				this.PseudoIndexMaintenanceRowsSeekRate = new ExPerformanceCounter(base.CategoryName, "PseudoIndexMaintenance - Seeks per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PseudoIndexMaintenanceRowsSeekRate, new ExPerformanceCounter[0]);
				list.Add(this.PseudoIndexMaintenanceRowsSeekRate);
				this.PseudoIndexMaintenanceRowsAcceptRate = new ExPerformanceCounter(base.CategoryName, "PseudoIndexMaintenance - Rows accepted per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PseudoIndexMaintenanceRowsAcceptRate, new ExPerformanceCounter[0]);
				list.Add(this.PseudoIndexMaintenanceRowsAcceptRate);
				this.PseudoIndexMaintenanceRowsWriteRate = new ExPerformanceCounter(base.CategoryName, "PseudoIndexMaintenance - Rows written per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PseudoIndexMaintenanceRowsWriteRate, new ExPerformanceCounter[0]);
				list.Add(this.PseudoIndexMaintenanceRowsWriteRate);
				this.PseudoIndexMaintenanceBytesReadRate = new ExPerformanceCounter(base.CategoryName, "PseudoIndexMaintenance - Bytes read per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PseudoIndexMaintenanceBytesReadRate, new ExPerformanceCounter[0]);
				list.Add(this.PseudoIndexMaintenanceBytesReadRate);
				this.PseudoIndexMaintenanceBytesWriteRate = new ExPerformanceCounter(base.CategoryName, "PseudoIndexMaintenance - Bytes written per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.PseudoIndexMaintenanceBytesWriteRate, new ExPerformanceCounter[0]);
				list.Add(this.PseudoIndexMaintenanceBytesWriteRate);
				this.EventsRowsReadRate = new ExPerformanceCounter(base.CategoryName, "Events - Rows read per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.EventsRowsReadRate, new ExPerformanceCounter[0]);
				list.Add(this.EventsRowsReadRate);
				this.EventsRowsSeekRate = new ExPerformanceCounter(base.CategoryName, "Events - Seeks per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.EventsRowsSeekRate, new ExPerformanceCounter[0]);
				list.Add(this.EventsRowsSeekRate);
				this.EventsRowsAcceptRate = new ExPerformanceCounter(base.CategoryName, "Events - Rows accepted per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.EventsRowsAcceptRate, new ExPerformanceCounter[0]);
				list.Add(this.EventsRowsAcceptRate);
				this.EventsRowsWriteRate = new ExPerformanceCounter(base.CategoryName, "Events - Rows written per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.EventsRowsWriteRate, new ExPerformanceCounter[0]);
				list.Add(this.EventsRowsWriteRate);
				this.EventsBytesReadRate = new ExPerformanceCounter(base.CategoryName, "Events - Bytes read per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.EventsBytesReadRate, new ExPerformanceCounter[0]);
				list.Add(this.EventsBytesReadRate);
				this.EventsBytesWriteRate = new ExPerformanceCounter(base.CategoryName, "Events - Bytes written per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.EventsBytesWriteRate, new ExPerformanceCounter[0]);
				list.Add(this.EventsBytesWriteRate);
				this.RowsReadRate = new ExPerformanceCounter(base.CategoryName, "Total - Rows read per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RowsReadRate, new ExPerformanceCounter[0]);
				list.Add(this.RowsReadRate);
				this.RowsSeekRate = new ExPerformanceCounter(base.CategoryName, "Total - Seeks per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RowsSeekRate, new ExPerformanceCounter[0]);
				list.Add(this.RowsSeekRate);
				this.RowsAcceptRate = new ExPerformanceCounter(base.CategoryName, "Total - Rows accepted per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RowsAcceptRate, new ExPerformanceCounter[0]);
				list.Add(this.RowsAcceptRate);
				this.RowsWriteRate = new ExPerformanceCounter(base.CategoryName, "Total - Rows written per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RowsWriteRate, new ExPerformanceCounter[0]);
				list.Add(this.RowsWriteRate);
				this.BytesReadRate = new ExPerformanceCounter(base.CategoryName, "Total - Bytes read per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.BytesReadRate, new ExPerformanceCounter[0]);
				list.Add(this.BytesReadRate);
				this.BytesWriteRate = new ExPerformanceCounter(base.CategoryName, "Total - Bytes written per second", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.BytesWriteRate, new ExPerformanceCounter[0]);
				list.Add(this.BytesWriteRate);
				long num = this.NumberOfQueriesPerSec.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter in list)
					{
						exPerformanceCounter.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000A7E4 File Offset: 0x000089E4
		internal PhysicalAccessPerformanceCountersInstance(string instanceName) : base(instanceName, "MSExchangeIS Physical Access")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.NumberOfQueriesPerSec = new ExPerformanceCounter(base.CategoryName, "Number of Queries per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfQueriesPerSec);
				this.NumberOfInsertsPerSec = new ExPerformanceCounter(base.CategoryName, "Number of Inserts per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfInsertsPerSec);
				this.NumberOfUpdatesPerSec = new ExPerformanceCounter(base.CategoryName, "Number of Updates per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfUpdatesPerSec);
				this.NumberOfDeletesPerSec = new ExPerformanceCounter(base.CategoryName, "Number of Deletes per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfDeletesPerSec);
				this.NumberOfOthersPerSec = new ExPerformanceCounter(base.CategoryName, "Number of Others per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfOthersPerSec);
				this.OffPageBlobHitsPerSec = new ExPerformanceCounter(base.CategoryName, "Number of off-page blob hits per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.OffPageBlobHitsPerSec);
				this.OtherRowsReadRate = new ExPerformanceCounter(base.CategoryName, "Other - Rows read per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.OtherRowsReadRate);
				this.OtherRowsSeekRate = new ExPerformanceCounter(base.CategoryName, "Other - Seeks per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.OtherRowsSeekRate);
				this.OtherRowsAcceptRate = new ExPerformanceCounter(base.CategoryName, "Other - Rows accepted per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.OtherRowsAcceptRate);
				this.OtherRowsWriteRate = new ExPerformanceCounter(base.CategoryName, "Other - Rows written per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.OtherRowsWriteRate);
				this.OtherBytesReadRate = new ExPerformanceCounter(base.CategoryName, "Other - Bytes read per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.OtherBytesReadRate);
				this.OtherBytesWriteRate = new ExPerformanceCounter(base.CategoryName, "Other - Bytes written per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.OtherBytesWriteRate);
				this.TableFunctionRowsReadRate = new ExPerformanceCounter(base.CategoryName, "TableFunction - Rows read per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TableFunctionRowsReadRate);
				this.TableFunctionRowsAcceptRate = new ExPerformanceCounter(base.CategoryName, "TableFunction - Rows accepted per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TableFunctionRowsAcceptRate);
				this.TableFunctionBytesReadRate = new ExPerformanceCounter(base.CategoryName, "TableFunction - Bytes read per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TableFunctionBytesReadRate);
				this.TempRowsReadRate = new ExPerformanceCounter(base.CategoryName, "Temp - Rows read per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TempRowsReadRate);
				this.TempRowsSeekRate = new ExPerformanceCounter(base.CategoryName, "Temp - Seeks per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TempRowsSeekRate);
				this.TempRowsAcceptRate = new ExPerformanceCounter(base.CategoryName, "Temp - Rows accepted per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TempRowsAcceptRate);
				this.TempRowsWriteRate = new ExPerformanceCounter(base.CategoryName, "Temp - Rows written per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TempRowsWriteRate);
				this.TempBytesReadRate = new ExPerformanceCounter(base.CategoryName, "Temp - Bytes read per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TempBytesReadRate);
				this.TempBytesWriteRate = new ExPerformanceCounter(base.CategoryName, "Temp - Bytes written per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TempBytesWriteRate);
				this.LazyIndexRowsReadRate = new ExPerformanceCounter(base.CategoryName, "LazyIndex - Rows read per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.LazyIndexRowsReadRate);
				this.LazyIndexRowsSeekRate = new ExPerformanceCounter(base.CategoryName, "LazyIndex - Seeks per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.LazyIndexRowsSeekRate);
				this.LazyIndexRowsAcceptRate = new ExPerformanceCounter(base.CategoryName, "LazyIndex - Rows accepted per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.LazyIndexRowsAcceptRate);
				this.LazyIndexRowsWriteRate = new ExPerformanceCounter(base.CategoryName, "LazyIndex - Rows written per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.LazyIndexRowsWriteRate);
				this.LazyIndexBytesReadRate = new ExPerformanceCounter(base.CategoryName, "LazyIndex - Bytes read per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.LazyIndexBytesReadRate);
				this.LazyIndexBytesWriteRate = new ExPerformanceCounter(base.CategoryName, "LazyIndex - Bytes written per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.LazyIndexBytesWriteRate);
				this.FolderRowsReadRate = new ExPerformanceCounter(base.CategoryName, "Folder - Rows read per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.FolderRowsReadRate);
				this.FolderRowsSeekRate = new ExPerformanceCounter(base.CategoryName, "Folder - Seeks per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.FolderRowsSeekRate);
				this.FolderRowsAcceptRate = new ExPerformanceCounter(base.CategoryName, "Folder - Rows accepted per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.FolderRowsAcceptRate);
				this.FolderRowsWriteRate = new ExPerformanceCounter(base.CategoryName, "Folder - Rows written per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.FolderRowsWriteRate);
				this.FolderBytesReadRate = new ExPerformanceCounter(base.CategoryName, "Folder - Bytes read per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.FolderBytesReadRate);
				this.FolderBytesWriteRate = new ExPerformanceCounter(base.CategoryName, "Folder - Bytes written per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.FolderBytesWriteRate);
				this.MessageRowsReadRate = new ExPerformanceCounter(base.CategoryName, "Message - Rows read per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MessageRowsReadRate);
				this.MessageRowsSeekRate = new ExPerformanceCounter(base.CategoryName, "Message - Seeks per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MessageRowsSeekRate);
				this.MessageRowsAcceptRate = new ExPerformanceCounter(base.CategoryName, "Message - Rows accepted per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MessageRowsAcceptRate);
				this.MessageRowsWriteRate = new ExPerformanceCounter(base.CategoryName, "Message - Rows written per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MessageRowsWriteRate);
				this.MessageBytesReadRate = new ExPerformanceCounter(base.CategoryName, "Message - Bytes read per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MessageBytesReadRate);
				this.MessageBytesWriteRate = new ExPerformanceCounter(base.CategoryName, "Message - Bytes written per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MessageBytesWriteRate);
				this.AttachmentRowsReadRate = new ExPerformanceCounter(base.CategoryName, "Attachment - Rows read per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AttachmentRowsReadRate);
				this.AttachmentRowsSeekRate = new ExPerformanceCounter(base.CategoryName, "Attachment - Seeks per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AttachmentRowsSeekRate);
				this.AttachmentRowsAcceptRate = new ExPerformanceCounter(base.CategoryName, "Attachment - Rows accepted per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AttachmentRowsAcceptRate);
				this.AttachmentRowsWriteRate = new ExPerformanceCounter(base.CategoryName, "Attachment - Rows written per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AttachmentRowsWriteRate);
				this.AttachmentBytesReadRate = new ExPerformanceCounter(base.CategoryName, "Attachment - Bytes read per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AttachmentBytesReadRate);
				this.AttachmentBytesWriteRate = new ExPerformanceCounter(base.CategoryName, "Attachment - Bytes written per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AttachmentBytesWriteRate);
				this.PseudoIndexMaintenanceRowsReadRate = new ExPerformanceCounter(base.CategoryName, "PseudoIndexMaintenance - Rows read per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PseudoIndexMaintenanceRowsReadRate);
				this.PseudoIndexMaintenanceRowsSeekRate = new ExPerformanceCounter(base.CategoryName, "PseudoIndexMaintenance - Seeks per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PseudoIndexMaintenanceRowsSeekRate);
				this.PseudoIndexMaintenanceRowsAcceptRate = new ExPerformanceCounter(base.CategoryName, "PseudoIndexMaintenance - Rows accepted per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PseudoIndexMaintenanceRowsAcceptRate);
				this.PseudoIndexMaintenanceRowsWriteRate = new ExPerformanceCounter(base.CategoryName, "PseudoIndexMaintenance - Rows written per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PseudoIndexMaintenanceRowsWriteRate);
				this.PseudoIndexMaintenanceBytesReadRate = new ExPerformanceCounter(base.CategoryName, "PseudoIndexMaintenance - Bytes read per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PseudoIndexMaintenanceBytesReadRate);
				this.PseudoIndexMaintenanceBytesWriteRate = new ExPerformanceCounter(base.CategoryName, "PseudoIndexMaintenance - Bytes written per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.PseudoIndexMaintenanceBytesWriteRate);
				this.EventsRowsReadRate = new ExPerformanceCounter(base.CategoryName, "Events - Rows read per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.EventsRowsReadRate);
				this.EventsRowsSeekRate = new ExPerformanceCounter(base.CategoryName, "Events - Seeks per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.EventsRowsSeekRate);
				this.EventsRowsAcceptRate = new ExPerformanceCounter(base.CategoryName, "Events - Rows accepted per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.EventsRowsAcceptRate);
				this.EventsRowsWriteRate = new ExPerformanceCounter(base.CategoryName, "Events - Rows written per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.EventsRowsWriteRate);
				this.EventsBytesReadRate = new ExPerformanceCounter(base.CategoryName, "Events - Bytes read per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.EventsBytesReadRate);
				this.EventsBytesWriteRate = new ExPerformanceCounter(base.CategoryName, "Events - Bytes written per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.EventsBytesWriteRate);
				this.RowsReadRate = new ExPerformanceCounter(base.CategoryName, "Total - Rows read per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RowsReadRate);
				this.RowsSeekRate = new ExPerformanceCounter(base.CategoryName, "Total - Seeks per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RowsSeekRate);
				this.RowsAcceptRate = new ExPerformanceCounter(base.CategoryName, "Total - Rows accepted per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RowsAcceptRate);
				this.RowsWriteRate = new ExPerformanceCounter(base.CategoryName, "Total - Rows written per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RowsWriteRate);
				this.BytesReadRate = new ExPerformanceCounter(base.CategoryName, "Total - Bytes read per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.BytesReadRate);
				this.BytesWriteRate = new ExPerformanceCounter(base.CategoryName, "Total - Bytes written per second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.BytesWriteRate);
				long num = this.NumberOfQueriesPerSec.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter in list)
					{
						exPerformanceCounter.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000B2E8 File Offset: 0x000094E8
		public override void GetPerfCounterDiagnosticsInfo(XElement topElement)
		{
			XElement xelement = null;
			foreach (ExPerformanceCounter exPerformanceCounter in this.counters)
			{
				try
				{
					if (xelement == null)
					{
						xelement = new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.InstanceName));
						topElement.Add(xelement);
					}
					xelement.Add(new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.CounterName), exPerformanceCounter.NextValue()));
				}
				catch (XmlException ex)
				{
					XElement content = new XElement("Error", ex.Message);
					topElement.Add(content);
				}
			}
		}

		// Token: 0x04000009 RID: 9
		public readonly ExPerformanceCounter NumberOfQueriesPerSec;

		// Token: 0x0400000A RID: 10
		public readonly ExPerformanceCounter NumberOfInsertsPerSec;

		// Token: 0x0400000B RID: 11
		public readonly ExPerformanceCounter NumberOfUpdatesPerSec;

		// Token: 0x0400000C RID: 12
		public readonly ExPerformanceCounter NumberOfDeletesPerSec;

		// Token: 0x0400000D RID: 13
		public readonly ExPerformanceCounter NumberOfOthersPerSec;

		// Token: 0x0400000E RID: 14
		public readonly ExPerformanceCounter OffPageBlobHitsPerSec;

		// Token: 0x0400000F RID: 15
		public readonly ExPerformanceCounter OtherRowsReadRate;

		// Token: 0x04000010 RID: 16
		public readonly ExPerformanceCounter OtherRowsSeekRate;

		// Token: 0x04000011 RID: 17
		public readonly ExPerformanceCounter OtherRowsAcceptRate;

		// Token: 0x04000012 RID: 18
		public readonly ExPerformanceCounter OtherRowsWriteRate;

		// Token: 0x04000013 RID: 19
		public readonly ExPerformanceCounter OtherBytesReadRate;

		// Token: 0x04000014 RID: 20
		public readonly ExPerformanceCounter OtherBytesWriteRate;

		// Token: 0x04000015 RID: 21
		public readonly ExPerformanceCounter TableFunctionRowsReadRate;

		// Token: 0x04000016 RID: 22
		public readonly ExPerformanceCounter TableFunctionRowsAcceptRate;

		// Token: 0x04000017 RID: 23
		public readonly ExPerformanceCounter TableFunctionBytesReadRate;

		// Token: 0x04000018 RID: 24
		public readonly ExPerformanceCounter TempRowsReadRate;

		// Token: 0x04000019 RID: 25
		public readonly ExPerformanceCounter TempRowsSeekRate;

		// Token: 0x0400001A RID: 26
		public readonly ExPerformanceCounter TempRowsAcceptRate;

		// Token: 0x0400001B RID: 27
		public readonly ExPerformanceCounter TempRowsWriteRate;

		// Token: 0x0400001C RID: 28
		public readonly ExPerformanceCounter TempBytesReadRate;

		// Token: 0x0400001D RID: 29
		public readonly ExPerformanceCounter TempBytesWriteRate;

		// Token: 0x0400001E RID: 30
		public readonly ExPerformanceCounter LazyIndexRowsReadRate;

		// Token: 0x0400001F RID: 31
		public readonly ExPerformanceCounter LazyIndexRowsSeekRate;

		// Token: 0x04000020 RID: 32
		public readonly ExPerformanceCounter LazyIndexRowsAcceptRate;

		// Token: 0x04000021 RID: 33
		public readonly ExPerformanceCounter LazyIndexRowsWriteRate;

		// Token: 0x04000022 RID: 34
		public readonly ExPerformanceCounter LazyIndexBytesReadRate;

		// Token: 0x04000023 RID: 35
		public readonly ExPerformanceCounter LazyIndexBytesWriteRate;

		// Token: 0x04000024 RID: 36
		public readonly ExPerformanceCounter FolderRowsReadRate;

		// Token: 0x04000025 RID: 37
		public readonly ExPerformanceCounter FolderRowsSeekRate;

		// Token: 0x04000026 RID: 38
		public readonly ExPerformanceCounter FolderRowsAcceptRate;

		// Token: 0x04000027 RID: 39
		public readonly ExPerformanceCounter FolderRowsWriteRate;

		// Token: 0x04000028 RID: 40
		public readonly ExPerformanceCounter FolderBytesReadRate;

		// Token: 0x04000029 RID: 41
		public readonly ExPerformanceCounter FolderBytesWriteRate;

		// Token: 0x0400002A RID: 42
		public readonly ExPerformanceCounter MessageRowsReadRate;

		// Token: 0x0400002B RID: 43
		public readonly ExPerformanceCounter MessageRowsSeekRate;

		// Token: 0x0400002C RID: 44
		public readonly ExPerformanceCounter MessageRowsAcceptRate;

		// Token: 0x0400002D RID: 45
		public readonly ExPerformanceCounter MessageRowsWriteRate;

		// Token: 0x0400002E RID: 46
		public readonly ExPerformanceCounter MessageBytesReadRate;

		// Token: 0x0400002F RID: 47
		public readonly ExPerformanceCounter MessageBytesWriteRate;

		// Token: 0x04000030 RID: 48
		public readonly ExPerformanceCounter AttachmentRowsReadRate;

		// Token: 0x04000031 RID: 49
		public readonly ExPerformanceCounter AttachmentRowsSeekRate;

		// Token: 0x04000032 RID: 50
		public readonly ExPerformanceCounter AttachmentRowsAcceptRate;

		// Token: 0x04000033 RID: 51
		public readonly ExPerformanceCounter AttachmentRowsWriteRate;

		// Token: 0x04000034 RID: 52
		public readonly ExPerformanceCounter AttachmentBytesReadRate;

		// Token: 0x04000035 RID: 53
		public readonly ExPerformanceCounter AttachmentBytesWriteRate;

		// Token: 0x04000036 RID: 54
		public readonly ExPerformanceCounter PseudoIndexMaintenanceRowsReadRate;

		// Token: 0x04000037 RID: 55
		public readonly ExPerformanceCounter PseudoIndexMaintenanceRowsSeekRate;

		// Token: 0x04000038 RID: 56
		public readonly ExPerformanceCounter PseudoIndexMaintenanceRowsAcceptRate;

		// Token: 0x04000039 RID: 57
		public readonly ExPerformanceCounter PseudoIndexMaintenanceRowsWriteRate;

		// Token: 0x0400003A RID: 58
		public readonly ExPerformanceCounter PseudoIndexMaintenanceBytesReadRate;

		// Token: 0x0400003B RID: 59
		public readonly ExPerformanceCounter PseudoIndexMaintenanceBytesWriteRate;

		// Token: 0x0400003C RID: 60
		public readonly ExPerformanceCounter EventsRowsReadRate;

		// Token: 0x0400003D RID: 61
		public readonly ExPerformanceCounter EventsRowsSeekRate;

		// Token: 0x0400003E RID: 62
		public readonly ExPerformanceCounter EventsRowsAcceptRate;

		// Token: 0x0400003F RID: 63
		public readonly ExPerformanceCounter EventsRowsWriteRate;

		// Token: 0x04000040 RID: 64
		public readonly ExPerformanceCounter EventsBytesReadRate;

		// Token: 0x04000041 RID: 65
		public readonly ExPerformanceCounter EventsBytesWriteRate;

		// Token: 0x04000042 RID: 66
		public readonly ExPerformanceCounter RowsReadRate;

		// Token: 0x04000043 RID: 67
		public readonly ExPerformanceCounter RowsSeekRate;

		// Token: 0x04000044 RID: 68
		public readonly ExPerformanceCounter RowsAcceptRate;

		// Token: 0x04000045 RID: 69
		public readonly ExPerformanceCounter RowsWriteRate;

		// Token: 0x04000046 RID: 70
		public readonly ExPerformanceCounter BytesReadRate;

		// Token: 0x04000047 RID: 71
		public readonly ExPerformanceCounter BytesWriteRate;
	}
}
