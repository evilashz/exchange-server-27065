using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Diagnostics.Components.BackSync;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020007C9 RID: 1993
	internal sealed class ObjectFullSyncPageToken : IFullSyncPageToken, ISyncCookie
	{
		// Token: 0x06006324 RID: 25380 RVA: 0x00157F90 File Offset: 0x00156190
		public ObjectFullSyncPageToken(Guid invocationId, ICollection<SyncObjectId> objectIds, BackSyncOptions syncOptions, ServiceInstanceId serviceInstanceId) : this(invocationId, objectIds, syncOptions, DateTime.UtcNow, DateTime.MinValue, null, null, serviceInstanceId, Guid.NewGuid(), DateTime.UtcNow)
		{
			ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "New ObjectFullSyncPageToken");
		}

		// Token: 0x06006325 RID: 25381 RVA: 0x00157FD4 File Offset: 0x001561D4
		public ObjectFullSyncPageToken(Guid invocationId, ICollection<SyncObjectId> objectIds, BackSyncOptions syncOptions, DateTime timestamp, DateTime lastReadFailureStartTime, FullSyncObjectCookie objectCookie, Dictionary<string, int> errorObjectsAndCount, ServiceInstanceId serviceInstanceId, Guid sequenceId, DateTime sequenceStartTimestamp)
		{
			this.Version = 2;
			ExTraceGlobals.BackSyncTracer.TraceDebug<int>((long)SyncConfiguration.TraceId, "Version {0}", this.Version);
			this.Timestamp = timestamp;
			ExTraceGlobals.BackSyncTracer.TraceDebug<DateTime>((long)SyncConfiguration.TraceId, "Timestamp {0}", this.Timestamp);
			this.LastReadFailureStartTime = lastReadFailureStartTime;
			ExTraceGlobals.BackSyncTracer.TraceDebug<DateTime>((long)SyncConfiguration.TraceId, "LastReadFailureStartTime {0}", this.LastReadFailureStartTime);
			this.InvocationId = invocationId;
			ExTraceGlobals.BackSyncTracer.TraceDebug<Guid>((long)SyncConfiguration.TraceId, "InvocationId {0}", this.InvocationId);
			this.ObjectIds = new HashSet<SyncObjectId>(objectIds);
			ExTraceGlobals.BackSyncTracer.TraceDebug<int>((long)SyncConfiguration.TraceId, "ObjectIds count = {0}", this.ObjectIds.Count);
			this.SyncOptions = syncOptions;
			ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "SyncOptions {0}", this.SyncOptions.ToString());
			this.ServiceInstanceId = serviceInstanceId;
			ExTraceGlobals.BackSyncTracer.TraceDebug<ServiceInstanceId>((long)SyncConfiguration.TraceId, "SyncServiceInstanceId {0}", this.ServiceInstanceId);
			this.ObjectCookie = objectCookie;
			this.ErrorObjectsAndFailureCounts = (errorObjectsAndCount ?? new Dictionary<string, int>());
			this.SequenceId = sequenceId;
			this.SequenceStartTimestamp = sequenceStartTimestamp;
			ExTraceGlobals.BackSyncTracer.TraceDebug<Guid, DateTime>((long)SyncConfiguration.TraceId, "BackSyncCookie this.SequenceId = {0} this.SequenceStartTimestamp = {1} ", this.SequenceId, this.SequenceStartTimestamp);
		}

		// Token: 0x1700233E RID: 9022
		// (get) Token: 0x06006326 RID: 25382 RVA: 0x00158136 File Offset: 0x00156336
		// (set) Token: 0x06006327 RID: 25383 RVA: 0x0015813E File Offset: 0x0015633E
		public BackSyncOptions SyncOptions { get; private set; }

		// Token: 0x1700233F RID: 9023
		// (get) Token: 0x06006328 RID: 25384 RVA: 0x00158147 File Offset: 0x00156347
		public bool MoreData
		{
			get
			{
				return this.ObjectIds.Count > 0;
			}
		}

		// Token: 0x17002340 RID: 9024
		// (get) Token: 0x06006329 RID: 25385 RVA: 0x00158157 File Offset: 0x00156357
		// (set) Token: 0x0600632A RID: 25386 RVA: 0x0015815F File Offset: 0x0015635F
		public DateTime Timestamp { get; set; }

		// Token: 0x17002341 RID: 9025
		// (get) Token: 0x0600632B RID: 25387 RVA: 0x00158168 File Offset: 0x00156368
		// (set) Token: 0x0600632C RID: 25388 RVA: 0x00158170 File Offset: 0x00156370
		public DateTime LastReadFailureStartTime { get; set; }

		// Token: 0x17002342 RID: 9026
		// (get) Token: 0x0600632D RID: 25389 RVA: 0x00158179 File Offset: 0x00156379
		// (set) Token: 0x0600632E RID: 25390 RVA: 0x00158181 File Offset: 0x00156381
		public Dictionary<string, int> ErrorObjectsAndFailureCounts { get; private set; }

		// Token: 0x17002343 RID: 9027
		// (get) Token: 0x0600632F RID: 25391 RVA: 0x0015818A File Offset: 0x0015638A
		// (set) Token: 0x06006330 RID: 25392 RVA: 0x00158192 File Offset: 0x00156392
		internal ServiceInstanceId ServiceInstanceId { get; private set; }

		// Token: 0x17002344 RID: 9028
		// (get) Token: 0x06006331 RID: 25393 RVA: 0x0015819B File Offset: 0x0015639B
		// (set) Token: 0x06006332 RID: 25394 RVA: 0x001581A3 File Offset: 0x001563A3
		public DateTime SequenceStartTimestamp { get; private set; }

		// Token: 0x17002345 RID: 9029
		// (get) Token: 0x06006333 RID: 25395 RVA: 0x001581AC File Offset: 0x001563AC
		// (set) Token: 0x06006334 RID: 25396 RVA: 0x001581B4 File Offset: 0x001563B4
		public Guid SequenceId { get; private set; }

		// Token: 0x06006335 RID: 25397 RVA: 0x001581C0 File Offset: 0x001563C0
		public byte[] ToByteArray()
		{
			byte[] result = null;
			using (BackSyncCookieWriter backSyncCookieWriter = BackSyncCookieWriter.Create(typeof(ObjectFullSyncPageToken)))
			{
				backSyncCookieWriter.WriteNextAttributeValue(this.Version);
				backSyncCookieWriter.WriteNextAttributeValue(this.ServiceInstanceId.InstanceId);
				backSyncCookieWriter.WriteNextAttributeValue(this.Timestamp.ToBinary());
				backSyncCookieWriter.WriteNextAttributeValue(this.LastReadFailureStartTime.ToBinary());
				backSyncCookieWriter.WriteNextAttributeValue(this.InvocationId);
				backSyncCookieWriter.WriteNextAttributeValue((int)this.SyncOptions);
				List<string> list = new List<string>();
				foreach (SyncObjectId syncObjectId in this.ObjectIds)
				{
					list.Add(syncObjectId.ToString());
				}
				backSyncCookieWriter.WriteNextAttributeValue(list.ToArray());
				if (this.ObjectCookie != null)
				{
					backSyncCookieWriter.WriteNextAttributeValue(this.ObjectCookie.ToByteArray());
				}
				else
				{
					backSyncCookieWriter.WriteNextAttributeValue(null);
				}
				string[] attributeValue = BackSyncCookie.ConvertErrorObjectsAndFailureCountsToArray(this.ErrorObjectsAndFailureCounts);
				backSyncCookieWriter.WriteNextAttributeValue(attributeValue);
				backSyncCookieWriter.WriteNextAttributeValue(this.SequenceStartTimestamp.ToBinary());
				backSyncCookieWriter.WriteNextAttributeValue(this.SequenceId);
				result = backSyncCookieWriter.GetBinaryCookie();
			}
			return result;
		}

		// Token: 0x06006336 RID: 25398 RVA: 0x00158350 File Offset: 0x00156550
		public void PrepareForFailover()
		{
			ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "ObjectFullSyncPageToken PrepareForFailover ...");
		}

		// Token: 0x17002346 RID: 9030
		// (get) Token: 0x06006337 RID: 25399 RVA: 0x00158367 File Offset: 0x00156567
		// (set) Token: 0x06006338 RID: 25400 RVA: 0x0015836F File Offset: 0x0015656F
		public int Version { get; private set; }

		// Token: 0x17002347 RID: 9031
		// (get) Token: 0x06006339 RID: 25401 RVA: 0x00158378 File Offset: 0x00156578
		// (set) Token: 0x0600633A RID: 25402 RVA: 0x00158380 File Offset: 0x00156580
		public Guid InvocationId { get; private set; }

		// Token: 0x17002348 RID: 9032
		// (get) Token: 0x0600633B RID: 25403 RVA: 0x00158389 File Offset: 0x00156589
		// (set) Token: 0x0600633C RID: 25404 RVA: 0x00158391 File Offset: 0x00156591
		public ICollection<SyncObjectId> ObjectIds { get; private set; }

		// Token: 0x17002349 RID: 9033
		// (get) Token: 0x0600633D RID: 25405 RVA: 0x0015839A File Offset: 0x0015659A
		// (set) Token: 0x0600633E RID: 25406 RVA: 0x001583A2 File Offset: 0x001565A2
		public FullSyncObjectCookie ObjectCookie { get; internal set; }

		// Token: 0x0600633F RID: 25407 RVA: 0x001583AC File Offset: 0x001565AC
		public void RemoveObjectFromList(SyncObjectId objectId, bool clearObjectCookie)
		{
			ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "ObjectFullSyncPageToken RemoveObjectFromList objectId {0}", (objectId != null) ? objectId.ObjectId : "NULL");
			this.ObjectIds.Remove(objectId);
			if (clearObjectCookie)
			{
				ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "ObjectFullSyncPageToken clear object cookie");
				this.ObjectCookie = null;
			}
		}

		// Token: 0x06006340 RID: 25408 RVA: 0x00158410 File Offset: 0x00156610
		internal static ObjectFullSyncPageToken Parse(byte[] tokenBytes)
		{
			if (tokenBytes == null)
			{
				throw new ArgumentNullException("tokenBytes");
			}
			Exception innerException;
			try
			{
				using (BackSyncCookieReader backSyncCookieReader = BackSyncCookieReader.Create(tokenBytes, typeof(ObjectFullSyncPageToken)))
				{
					int num = (int)backSyncCookieReader.GetNextAttributeValue();
					ServiceInstanceId serviceInstanceId = new ServiceInstanceId((string)backSyncCookieReader.GetNextAttributeValue());
					long dateData = (long)backSyncCookieReader.GetNextAttributeValue();
					long dateData2 = (long)backSyncCookieReader.GetNextAttributeValue();
					Guid invocationId = (Guid)backSyncCookieReader.GetNextAttributeValue();
					BackSyncOptions syncOptions = (BackSyncOptions)((int)backSyncCookieReader.GetNextAttributeValue());
					string[] array = (string[])backSyncCookieReader.GetNextAttributeValue();
					byte[] array2 = (byte[])backSyncCookieReader.GetNextAttributeValue();
					string[] errorObjects = (string[])backSyncCookieReader.GetNextAttributeValue();
					Dictionary<string, int> errorObjectsAndCount = BackSyncCookie.ParseErrorObjectsAndFailureCounts(errorObjects);
					DateTime sequenceStartTimestamp = DateTime.FromBinary((long)backSyncCookieReader.GetNextAttributeValue());
					Guid sequenceId = (Guid)backSyncCookieReader.GetNextAttributeValue();
					HashSet<SyncObjectId> hashSet = new HashSet<SyncObjectId>();
					if (array != null)
					{
						foreach (string identity in array)
						{
							hashSet.Add(SyncObjectId.Parse(identity));
						}
					}
					FullSyncObjectCookie objectCookie = (array2 == null) ? null : FullSyncObjectCookie.Parse(array2);
					return new ObjectFullSyncPageToken(invocationId, hashSet, syncOptions, DateTime.FromBinary(dateData), DateTime.FromBinary(dateData2), objectCookie, errorObjectsAndCount, serviceInstanceId, sequenceId, sequenceStartTimestamp);
				}
			}
			catch (ArgumentException ex)
			{
				innerException = ex;
			}
			catch (IOException ex2)
			{
				innerException = ex2;
			}
			catch (FormatException ex3)
			{
				innerException = ex3;
			}
			catch (InvalidCookieException ex4)
			{
				innerException = ex4;
			}
			throw new InvalidCookieException(innerException);
		}

		// Token: 0x04004228 RID: 16936
		internal const int CurrentVersion = 2;

		// Token: 0x04004229 RID: 16937
		internal static BackSyncCookieAttribute[] ObjectFullSyncPageTokenAttributeSchema_Version_1 = new BackSyncCookieAttribute[]
		{
			new BackSyncCookieAttribute
			{
				Name = "TimeStampRaw",
				DataType = typeof(long),
				DefaultValue = Convert.ToInt64(0)
			},
			new BackSyncCookieAttribute
			{
				Name = "LastReadFailureStartTimeRaw",
				DataType = typeof(long),
				DefaultValue = Convert.ToInt64(0)
			},
			new BackSyncCookieAttribute
			{
				Name = "InvocationId",
				DataType = typeof(Guid),
				DefaultValue = Guid.Empty
			},
			new BackSyncCookieAttribute
			{
				Name = "SyncOptions",
				DataType = typeof(int),
				DefaultValue = 0
			},
			new BackSyncCookieAttribute
			{
				Name = "ObjectIds",
				DataType = typeof(string[]),
				DefaultValue = null
			},
			new BackSyncCookieAttribute
			{
				Name = "ObjectCookie",
				DataType = typeof(byte[]),
				DefaultValue = null
			}
		};

		// Token: 0x0400422A RID: 16938
		internal static BackSyncCookieAttribute[] ObjectFullSyncPageTokenAttributeSchema_Version_2 = new BackSyncCookieAttribute[]
		{
			new BackSyncCookieAttribute
			{
				Name = "ErrorObjectsAndFailureCounts",
				DataType = typeof(string[]),
				DefaultValue = null
			}
		};

		// Token: 0x0400422B RID: 16939
		internal static BackSyncCookieAttribute[] ObjectFullSyncPageTokenAttributeSchema_Version_3 = new BackSyncCookieAttribute[]
		{
			new BackSyncCookieAttribute
			{
				Name = "SequenceStartTimeRaw",
				DataType = typeof(long),
				DefaultValue = Convert.ToInt64(0)
			},
			new BackSyncCookieAttribute
			{
				Name = "SequenceId",
				DataType = typeof(Guid),
				DefaultValue = Guid.Empty
			}
		};

		// Token: 0x0400422C RID: 16940
		internal static BackSyncCookieAttribute[][] ObjectFullSyncPageTokenAttributeSchemaByVersions = new BackSyncCookieAttribute[][]
		{
			BackSyncCookieAttribute.BackSyncCookieVersionSchema,
			ObjectFullSyncPageToken.ObjectFullSyncPageTokenAttributeSchema_Version_1,
			ObjectFullSyncPageToken.ObjectFullSyncPageTokenAttributeSchema_Version_2,
			ObjectFullSyncPageToken.ObjectFullSyncPageTokenAttributeSchema_Version_3
		};
	}
}
