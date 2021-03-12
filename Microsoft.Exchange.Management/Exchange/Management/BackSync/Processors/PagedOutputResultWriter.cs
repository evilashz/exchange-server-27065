using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Sync;
using Microsoft.Exchange.Diagnostics.Components.BackSync;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.BackSync.Processors
{
	// Token: 0x020000B9 RID: 185
	internal class PagedOutputResultWriter : IDataProcessor
	{
		// Token: 0x060005E9 RID: 1513 RVA: 0x0001986D File Offset: 0x00017A6D
		public PagedOutputResultWriter(WriteResultDelegate writeDelegate, Func<IEnumerable<SyncObject>, bool, byte[], ServiceInstanceId, object> createResponseDelegate, Action<int> serializeCountDelegate, AddErrorSyncObjectDelegate addErrorObjectDelegate, ServiceInstanceId currentServiceInstanceId)
		{
			this.writeDelegate = writeDelegate;
			this.createResponseDelegate = createResponseDelegate;
			this.serializeCountDelegate = serializeCountDelegate;
			this.entries = new List<PropertyBag>();
			this.addErrorSyncObjectDelegate = addErrorObjectDelegate;
			this.currentServiceInstanceId = currentServiceInstanceId;
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x000198A5 File Offset: 0x00017AA5
		public void Process(PropertyBag propertyBag)
		{
			this.CollectResult(propertyBag);
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x000198AE File Offset: 0x00017AAE
		public void Flush(Func<byte[]> getCookieDelegate, bool moreData)
		{
			this.writeDelegate(this.CreateOutputObject(getCookieDelegate(), moreData));
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x000198C8 File Offset: 0x00017AC8
		protected void CollectResult(PropertyBag propertyBag)
		{
			this.entries.Add(propertyBag);
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x000198D8 File Offset: 0x00017AD8
		private IConfigurable CreateOutputObject(byte[] cookie, bool moreData)
		{
			ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "PagedOutputResultWriter.CreateOutputObject entering");
			ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "Create SyncObject list from ADPropertyBag list ...");
			List<SyncObject> list = new List<SyncObject>();
			foreach (PropertyBag propertyBag in this.entries)
			{
				ProcessorHelper.TracePropertBag("<PagedOutputResultWriter::CreateOutputObject>", propertyBag);
				list.Add(SyncObject.Create((ADPropertyBag)propertyBag));
			}
			ExTraceGlobals.BackSyncTracer.TraceDebug<int>((long)SyncConfiguration.TraceId, "SyncObject count = {0}", list.Count);
			if (this.serializeCountDelegate != null)
			{
				this.serializeCountDelegate(this.entries.Count);
			}
			ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "Create SyncData from SyncObject list ...");
			SyncData result = null;
			try
			{
				result = new SyncData(cookie, this.createResponseDelegate(list, moreData, cookie, this.currentServiceInstanceId));
				ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "SyncData created successfully");
			}
			catch (Exception ex)
			{
				ExTraceGlobals.BackSyncTracer.TraceError<string>((long)SyncConfiguration.TraceId, "Encountered exception during SyncData creation {0}", ex.ToString());
				if (this.addErrorSyncObjectDelegate == null)
				{
					throw ex;
				}
				if (this.FindErrorSyncObjects(cookie, moreData, list) == 0)
				{
					ExTraceGlobals.BackSyncTracer.TraceError<string>((long)SyncConfiguration.TraceId, "Didn't find SyncObject that caused the exception. Re-throw exception {0}.", ex.ToString());
					throw ex;
				}
				ExTraceGlobals.BackSyncTracer.TraceError((long)SyncConfiguration.TraceId, "PagedOutputResultWriter.CreateOutputObject Throw DataSourceTransientException");
				throw new DataSourceTransientException(Strings.BackSyncFailedToConvertSyncObjectToDirectoryObject, ex);
			}
			return result;
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00019A74 File Offset: 0x00017C74
		private int FindErrorSyncObjects(byte[] cookie, bool moreData, List<SyncObject> syncObjects)
		{
			ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "PagedOutputResultWriter.FindErrorSyncObjects entering");
			int num = 0;
			foreach (SyncObject syncObject in syncObjects)
			{
				string objectId = syncObject.ObjectId;
				ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "PagedOutputResultWriter.FindErrorSyncObjects check {0}", objectId);
				try
				{
					List<SyncObject> list = new List<SyncObject>();
					list.Add(syncObject);
					new SyncData(cookie, this.createResponseDelegate(list, moreData, cookie, this.currentServiceInstanceId));
					ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "PagedOutputResultWriter.FindErrorSyncObjects passed {0}", objectId);
				}
				catch (Exception ex)
				{
					ExTraceGlobals.BackSyncTracer.TraceError<string, string>((long)SyncConfiguration.TraceId, "PagedOutputResultWriter.FindErrorSyncObjects failed {0} exception {1}", objectId, ex.ToString());
					this.addErrorSyncObjectDelegate(syncObject, ex);
					num++;
				}
			}
			ExTraceGlobals.BackSyncTracer.TraceDebug<int>((long)SyncConfiguration.TraceId, "PagedOutputResultWriter.FindErrorSyncObjects this.syncConfiguration.ErrorSyncObjects.count = {0}", num);
			return num;
		}

		// Token: 0x040002DF RID: 735
		private readonly WriteResultDelegate writeDelegate;

		// Token: 0x040002E0 RID: 736
		private readonly Func<IEnumerable<SyncObject>, bool, byte[], ServiceInstanceId, object> createResponseDelegate;

		// Token: 0x040002E1 RID: 737
		private readonly Action<int> serializeCountDelegate;

		// Token: 0x040002E2 RID: 738
		private readonly List<PropertyBag> entries;

		// Token: 0x040002E3 RID: 739
		private readonly AddErrorSyncObjectDelegate addErrorSyncObjectDelegate;

		// Token: 0x040002E4 RID: 740
		private readonly ServiceInstanceId currentServiceInstanceId;
	}
}
