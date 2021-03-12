using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Sync.TenantRelocationSync;
using Microsoft.Exchange.Data.Directory.TopologyDiscovery;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000809 RID: 2057
	internal class TenantRelocationSyncPageToken : IFullSyncPageToken, ISyncCookie
	{
		// Token: 0x06006558 RID: 25944 RVA: 0x00162CB4 File Offset: 0x00160EB4
		public TenantRelocationSyncPageToken(Guid invocationId, ADObjectId tenantOuId, ADObjectId tenantCuId, TenantPartitionHint partitionHint, bool isTenantCuInConfigNC)
		{
			ExTraceGlobals.TenantRelocationTracer.TraceDebug((long)this.TenantConfigUnitObjectGuid.GetHashCode(), "New TenantRelocationSyncPageToken");
			this.Version = 1;
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<int>((long)this.TenantConfigUnitObjectGuid.GetHashCode(), "TenantRelocationSyncPageToken this.Version = {0}", this.Version);
			this.InvocationId = invocationId;
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<Guid>((long)this.TenantConfigUnitObjectGuid.GetHashCode(), "TenantRelocationSyncPageToken this.InvocationId = {0}", this.InvocationId);
			this.TenantOrganizationalUnitObjectGuid = tenantOuId.ObjectGuid;
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<Guid>((long)this.TenantConfigUnitObjectGuid.GetHashCode(), "TenantRelocationSyncPageToken this.TenantOrganizationalUnitObjectGuid = {0}", this.TenantOrganizationalUnitObjectGuid);
			this.TenantConfigUnitObjectGuid = tenantCuId.ObjectGuid;
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<Guid>((long)this.TenantConfigUnitObjectGuid.GetHashCode(), "TenantRelocationSyncPageToken this.TenantConfigUnitObjectGuid = {0}", this.TenantConfigUnitObjectGuid);
			this.PartitionHint = partitionHint;
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<string>((long)this.TenantConfigUnitObjectGuid.GetHashCode(), "TenantRelocationSyncPageToken this.PartitionHint = {0}", this.PartitionHint.ToString());
			this.State = TenantRelocationSyncState.PreSyncAllObjects;
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<string>((long)this.TenantConfigUnitObjectGuid.GetHashCode(), "TenantRelocationSyncPageToken this.State = {0}", this.State.ToString());
			this.ErrorObjectsAndFailureCounts = new Dictionary<string, int>();
		}

		// Token: 0x170023DF RID: 9183
		// (get) Token: 0x06006559 RID: 25945 RVA: 0x00162E39 File Offset: 0x00161039
		// (set) Token: 0x0600655A RID: 25946 RVA: 0x00162E41 File Offset: 0x00161041
		public int Version { get; private set; }

		// Token: 0x170023E0 RID: 9184
		// (get) Token: 0x0600655B RID: 25947 RVA: 0x00162E4A File Offset: 0x0016104A
		// (set) Token: 0x0600655C RID: 25948 RVA: 0x00162E52 File Offset: 0x00161052
		public Guid TenantOrganizationalUnitObjectGuid { get; private set; }

		// Token: 0x170023E1 RID: 9185
		// (get) Token: 0x0600655D RID: 25949 RVA: 0x00162E5B File Offset: 0x0016105B
		// (set) Token: 0x0600655E RID: 25950 RVA: 0x00162E63 File Offset: 0x00161063
		public Guid TenantConfigUnitObjectGuid { get; private set; }

		// Token: 0x170023E2 RID: 9186
		// (get) Token: 0x0600655F RID: 25951 RVA: 0x00162E6C File Offset: 0x0016106C
		// (set) Token: 0x06006560 RID: 25952 RVA: 0x00162E74 File Offset: 0x00161074
		public bool IsTenantConfigUnitInConfigNc { get; set; }

		// Token: 0x170023E3 RID: 9187
		// (get) Token: 0x06006561 RID: 25953 RVA: 0x00162E7D File Offset: 0x0016107D
		// (set) Token: 0x06006562 RID: 25954 RVA: 0x00162E85 File Offset: 0x00161085
		public Guid InvocationId { get; internal set; }

		// Token: 0x170023E4 RID: 9188
		// (get) Token: 0x06006563 RID: 25955 RVA: 0x00162E8E File Offset: 0x0016108E
		// (set) Token: 0x06006564 RID: 25956 RVA: 0x00162E96 File Offset: 0x00161096
		public TenantRelocationSyncState State { get; private set; }

		// Token: 0x170023E5 RID: 9189
		// (get) Token: 0x06006565 RID: 25957 RVA: 0x00162E9F File Offset: 0x0016109F
		// (set) Token: 0x06006566 RID: 25958 RVA: 0x00162EA7 File Offset: 0x001610A7
		internal TenantPartitionHint PartitionHint { get; set; }

		// Token: 0x170023E6 RID: 9190
		// (get) Token: 0x06006567 RID: 25959 RVA: 0x00162EB0 File Offset: 0x001610B0
		// (set) Token: 0x06006568 RID: 25960 RVA: 0x00162EB8 File Offset: 0x001610B8
		internal long ConfigUnitObjectUSN { get; set; }

		// Token: 0x170023E7 RID: 9191
		// (get) Token: 0x06006569 RID: 25961 RVA: 0x00162EC1 File Offset: 0x001610C1
		// (set) Token: 0x0600656A RID: 25962 RVA: 0x00162EC9 File Offset: 0x001610C9
		internal long ConfigUnitTombstoneUSN { get; set; }

		// Token: 0x170023E8 RID: 9192
		// (get) Token: 0x0600656B RID: 25963 RVA: 0x00162ED2 File Offset: 0x001610D2
		// (set) Token: 0x0600656C RID: 25964 RVA: 0x00162EDA File Offset: 0x001610DA
		internal long OrganizationalUnitObjectUSN { get; set; }

		// Token: 0x170023E9 RID: 9193
		// (get) Token: 0x0600656D RID: 25965 RVA: 0x00162EE3 File Offset: 0x001610E3
		// (set) Token: 0x0600656E RID: 25966 RVA: 0x00162EEB File Offset: 0x001610EB
		internal long OrganizationalUnitTombstoneUSN { get; set; }

		// Token: 0x170023EA RID: 9194
		// (get) Token: 0x0600656F RID: 25967 RVA: 0x00162EF4 File Offset: 0x001610F4
		// (set) Token: 0x06006570 RID: 25968 RVA: 0x00162EFC File Offset: 0x001610FC
		internal long SpecialObjectsUSN { get; set; }

		// Token: 0x170023EB RID: 9195
		// (get) Token: 0x06006571 RID: 25969 RVA: 0x00162F05 File Offset: 0x00161105
		// (set) Token: 0x06006572 RID: 25970 RVA: 0x00162F0D File Offset: 0x0016110D
		internal long LinkPageStart { get; private set; }

		// Token: 0x170023EC RID: 9196
		// (get) Token: 0x06006573 RID: 25971 RVA: 0x00162F16 File Offset: 0x00161116
		// (set) Token: 0x06006574 RID: 25972 RVA: 0x00162F1E File Offset: 0x0016111E
		internal long LinkPageEnd { get; private set; }

		// Token: 0x170023ED RID: 9197
		// (get) Token: 0x06006575 RID: 25973 RVA: 0x00162F27 File Offset: 0x00161127
		// (set) Token: 0x06006576 RID: 25974 RVA: 0x00162F2F File Offset: 0x0016112F
		internal int LinkRangeStart { get; private set; }

		// Token: 0x170023EE RID: 9198
		// (get) Token: 0x06006577 RID: 25975 RVA: 0x00162F38 File Offset: 0x00161138
		// (set) Token: 0x06006578 RID: 25976 RVA: 0x00162F40 File Offset: 0x00161140
		internal int ObjectsInLinkPage { get; private set; }

		// Token: 0x170023EF RID: 9199
		// (get) Token: 0x06006579 RID: 25977 RVA: 0x00162F49 File Offset: 0x00161149
		// (set) Token: 0x0600657A RID: 25978 RVA: 0x00162F51 File Offset: 0x00161151
		internal WatermarkMap Watermarks { get; set; }

		// Token: 0x170023F0 RID: 9200
		// (get) Token: 0x0600657B RID: 25979 RVA: 0x00162F5A File Offset: 0x0016115A
		// (set) Token: 0x0600657C RID: 25980 RVA: 0x00162F62 File Offset: 0x00161162
		internal WatermarkMap PendingWatermarks { get; set; }

		// Token: 0x170023F1 RID: 9201
		// (get) Token: 0x0600657D RID: 25981 RVA: 0x00162F6B File Offset: 0x0016116B
		// (set) Token: 0x0600657E RID: 25982 RVA: 0x00162F73 File Offset: 0x00161173
		internal WatermarkMap ConfigNcWatermarks { get; set; }

		// Token: 0x170023F2 RID: 9202
		// (get) Token: 0x0600657F RID: 25983 RVA: 0x00162F7C File Offset: 0x0016117C
		// (set) Token: 0x06006580 RID: 25984 RVA: 0x00162F84 File Offset: 0x00161184
		internal WatermarkMap PendingConfigNcWatermarks { get; set; }

		// Token: 0x170023F3 RID: 9203
		// (get) Token: 0x06006581 RID: 25985 RVA: 0x00162F8D File Offset: 0x0016118D
		// (set) Token: 0x06006582 RID: 25986 RVA: 0x00162F95 File Offset: 0x00161195
		internal Guid WatermarksInvocationId { get; set; }

		// Token: 0x170023F4 RID: 9204
		// (get) Token: 0x06006583 RID: 25987 RVA: 0x00162F9E File Offset: 0x0016119E
		// (set) Token: 0x06006584 RID: 25988 RVA: 0x00162FA6 File Offset: 0x001611A6
		internal Guid PendingWatermarksInvocationId { get; set; }

		// Token: 0x170023F5 RID: 9205
		// (get) Token: 0x06006585 RID: 25989 RVA: 0x00162FAF File Offset: 0x001611AF
		// (set) Token: 0x06006586 RID: 25990 RVA: 0x00162FB7 File Offset: 0x001611B7
		public Dictionary<string, int> ErrorObjectsAndFailureCounts { get; private set; }

		// Token: 0x170023F6 RID: 9206
		// (get) Token: 0x06006587 RID: 25991 RVA: 0x00162FC0 File Offset: 0x001611C0
		// (set) Token: 0x06006588 RID: 25992 RVA: 0x00162FC8 File Offset: 0x001611C8
		public string AffinityDcFqdn { get; set; }

		// Token: 0x170023F7 RID: 9207
		// (get) Token: 0x06006589 RID: 25993 RVA: 0x00162FD1 File Offset: 0x001611D1
		// (set) Token: 0x0600658A RID: 25994 RVA: 0x00162FD9 File Offset: 0x001611D9
		public string AffinityTargetDcFqdn { get; set; }

		// Token: 0x170023F8 RID: 9208
		// (get) Token: 0x0600658B RID: 25995 RVA: 0x00162FE2 File Offset: 0x001611E2
		// (set) Token: 0x0600658C RID: 25996 RVA: 0x00162FEA File Offset: 0x001611EA
		public byte[] PreSyncLdapPagingCookie { get; set; }

		// Token: 0x170023F9 RID: 9209
		// (get) Token: 0x0600658D RID: 25997 RVA: 0x00162FF3 File Offset: 0x001611F3
		public DateTime SequenceStartTimestamp
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170023FA RID: 9210
		// (get) Token: 0x0600658E RID: 25998 RVA: 0x00162FFA File Offset: 0x001611FA
		public Guid SequenceId
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600658F RID: 25999 RVA: 0x00163001 File Offset: 0x00161201
		internal static TenantRelocationSyncPageToken Parse(byte[] tokenBytes)
		{
			if (tokenBytes == null)
			{
				throw new ArgumentNullException("tokenBytes");
			}
			return new TenantRelocationSyncPageToken(tokenBytes);
		}

		// Token: 0x06006590 RID: 26000 RVA: 0x00163018 File Offset: 0x00161218
		public TenantRelocationSyncPageToken(byte[] tokenBytes)
		{
			Exception ex = null;
			try
			{
				using (BackSyncCookieReader backSyncCookieReader = BackSyncCookieReader.Create(tokenBytes, typeof(TenantRelocationSyncPageToken)))
				{
					this.Version = (int)backSyncCookieReader.GetNextAttributeValue();
					backSyncCookieReader.GetNextAttributeValue();
					this.Timestamp = DateTime.FromBinary((long)backSyncCookieReader.GetNextAttributeValue());
					this.LastReadFailureStartTime = DateTime.FromBinary((long)backSyncCookieReader.GetNextAttributeValue());
					this.InvocationId = (Guid)backSyncCookieReader.GetNextAttributeValue();
					this.TenantConfigUnitObjectGuid = (Guid)backSyncCookieReader.GetNextAttributeValue();
					this.TenantOrganizationalUnitObjectGuid = (Guid)backSyncCookieReader.GetNextAttributeValue();
					this.IsTenantConfigUnitInConfigNc = (bool)backSyncCookieReader.GetNextAttributeValue();
					byte[] array = (byte[])backSyncCookieReader.GetNextAttributeValue();
					if (array != null)
					{
						this.PartitionHint = TenantPartitionHint.Deserialize(array);
					}
					this.State = (TenantRelocationSyncState)backSyncCookieReader.GetNextAttributeValue();
					this.ConfigUnitObjectUSN = (long)backSyncCookieReader.GetNextAttributeValue();
					this.ConfigUnitTombstoneUSN = (long)backSyncCookieReader.GetNextAttributeValue();
					this.OrganizationalUnitObjectUSN = (long)backSyncCookieReader.GetNextAttributeValue();
					this.OrganizationalUnitTombstoneUSN = (long)backSyncCookieReader.GetNextAttributeValue();
					this.SpecialObjectsUSN = (long)backSyncCookieReader.GetNextAttributeValue();
					byte[] array2 = (byte[])backSyncCookieReader.GetNextAttributeValue();
					if (array2 != null)
					{
						this.ConfigNcWatermarks = WatermarkMap.Parse(array2);
					}
					byte[] array3 = (byte[])backSyncCookieReader.GetNextAttributeValue();
					if (array3 != null)
					{
						this.PendingConfigNcWatermarks = WatermarkMap.Parse(array3);
					}
					byte[] array4 = (byte[])backSyncCookieReader.GetNextAttributeValue();
					if (array4 != null)
					{
						this.Watermarks = WatermarkMap.Parse(array4);
					}
					this.WatermarksInvocationId = (Guid)backSyncCookieReader.GetNextAttributeValue();
					this.PendingWatermarksInvocationId = (Guid)backSyncCookieReader.GetNextAttributeValue();
					byte[] array5 = (byte[])backSyncCookieReader.GetNextAttributeValue();
					if (array5 != null)
					{
						this.PendingWatermarks = WatermarkMap.Parse(array5);
					}
					this.LinkPageStart = (long)backSyncCookieReader.GetNextAttributeValue();
					this.LinkPageEnd = (long)backSyncCookieReader.GetNextAttributeValue();
					this.LinkRangeStart = (int)backSyncCookieReader.GetNextAttributeValue();
					this.ObjectsInLinkPage = (int)backSyncCookieReader.GetNextAttributeValue();
					this.AffinityDcFqdn = (string)backSyncCookieReader.GetNextAttributeValue();
					this.AffinityTargetDcFqdn = (string)backSyncCookieReader.GetNextAttributeValue();
					this.PreSyncLdapPagingCookie = (byte[])backSyncCookieReader.GetNextAttributeValue();
				}
			}
			catch (ArgumentException ex2)
			{
				ExTraceGlobals.TenantRelocationTracer.TraceError<string>((long)this.TenantConfigUnitObjectGuid.GetHashCode(), "TenantRelocationSyncPageToken ArgumentException {0}", ex2.ToString());
				ex = ex2;
			}
			catch (IOException ex3)
			{
				ExTraceGlobals.TenantRelocationTracer.TraceError<string>((long)this.TenantConfigUnitObjectGuid.GetHashCode(), "TenantRelocationSyncPageToken IOException {0}", ex3.ToString());
				ex = ex3;
			}
			catch (FormatException ex4)
			{
				ExTraceGlobals.TenantRelocationTracer.TraceError<string>((long)this.TenantConfigUnitObjectGuid.GetHashCode(), "TenantRelocationSyncPageToken FormatException {0}", ex4.ToString());
				ex = ex4;
			}
			catch (InvalidCookieException ex5)
			{
				ExTraceGlobals.TenantRelocationTracer.TraceError<string>((long)this.TenantConfigUnitObjectGuid.GetHashCode(), "TenantRelocationSyncPageToken InvalidCookieException {0}", ex5.ToString());
				ex = ex5;
			}
			if (ex != null)
			{
				ExTraceGlobals.TenantRelocationTracer.TraceError<string>((long)this.TenantConfigUnitObjectGuid.GetHashCode(), "TenantRelocationSyncPageToken throw InvalidCookieException {0}", ex.ToString());
				throw new InvalidCookieException(ex);
			}
		}

		// Token: 0x170023FB RID: 9211
		// (get) Token: 0x06006591 RID: 26001 RVA: 0x001633E8 File Offset: 0x001615E8
		public BackSyncOptions SyncOptions
		{
			get
			{
				return BackSyncOptions.IncludeLinks;
			}
		}

		// Token: 0x170023FC RID: 9212
		// (get) Token: 0x06006592 RID: 26002 RVA: 0x001633EB File Offset: 0x001615EB
		public bool IsPreSyncPhase
		{
			get
			{
				return this.State <= TenantRelocationSyncState.PreSyncAllObjects;
			}
		}

		// Token: 0x170023FD RID: 9213
		// (get) Token: 0x06006593 RID: 26003 RVA: 0x001633F9 File Offset: 0x001615F9
		public bool MoreData
		{
			get
			{
				return this.State != TenantRelocationSyncState.Complete;
			}
		}

		// Token: 0x170023FE RID: 9214
		// (get) Token: 0x06006594 RID: 26004 RVA: 0x00163407 File Offset: 0x00161607
		// (set) Token: 0x06006595 RID: 26005 RVA: 0x0016340F File Offset: 0x0016160F
		public DateTime Timestamp { get; set; }

		// Token: 0x170023FF RID: 9215
		// (get) Token: 0x06006596 RID: 26006 RVA: 0x00163418 File Offset: 0x00161618
		// (set) Token: 0x06006597 RID: 26007 RVA: 0x00163420 File Offset: 0x00161620
		public DateTime LastReadFailureStartTime { get; set; }

		// Token: 0x06006598 RID: 26008 RVA: 0x0016342C File Offset: 0x0016162C
		public virtual byte[] ToByteArray()
		{
			byte[] result = null;
			using (BackSyncCookieWriter backSyncCookieWriter = BackSyncCookieWriter.Create(typeof(TenantRelocationSyncPageToken)))
			{
				backSyncCookieWriter.WriteNextAttributeValue(this.Version);
				backSyncCookieWriter.WriteNextAttributeValue("Exchange/SDF");
				backSyncCookieWriter.WriteNextAttributeValue(this.Timestamp.ToBinary());
				backSyncCookieWriter.WriteNextAttributeValue(this.LastReadFailureStartTime.ToBinary());
				backSyncCookieWriter.WriteNextAttributeValue(this.InvocationId);
				backSyncCookieWriter.WriteNextAttributeValue(this.TenantConfigUnitObjectGuid);
				backSyncCookieWriter.WriteNextAttributeValue(this.TenantOrganizationalUnitObjectGuid);
				backSyncCookieWriter.WriteNextAttributeValue(this.IsTenantConfigUnitInConfigNc);
				if (this.PartitionHint == null)
				{
					backSyncCookieWriter.WriteNextAttributeValue(null);
				}
				backSyncCookieWriter.WriteNextAttributeValue(TenantPartitionHint.Serialize(this.PartitionHint));
				backSyncCookieWriter.WriteNextAttributeValue((int)this.State);
				backSyncCookieWriter.WriteNextAttributeValue(this.ConfigUnitObjectUSN);
				backSyncCookieWriter.WriteNextAttributeValue(this.ConfigUnitTombstoneUSN);
				backSyncCookieWriter.WriteNextAttributeValue(this.OrganizationalUnitObjectUSN);
				backSyncCookieWriter.WriteNextAttributeValue(this.OrganizationalUnitTombstoneUSN);
				backSyncCookieWriter.WriteNextAttributeValue(this.SpecialObjectsUSN);
				if (this.ConfigNcWatermarks == null)
				{
					backSyncCookieWriter.WriteNextAttributeValue(null);
				}
				else
				{
					byte[] attributeValue = this.ConfigNcWatermarks.SerializeToBytes();
					backSyncCookieWriter.WriteNextAttributeValue(attributeValue);
				}
				if (this.PendingConfigNcWatermarks == null)
				{
					backSyncCookieWriter.WriteNextAttributeValue(null);
				}
				else
				{
					byte[] attributeValue2 = this.PendingConfigNcWatermarks.SerializeToBytes();
					backSyncCookieWriter.WriteNextAttributeValue(attributeValue2);
				}
				if (this.Watermarks == null)
				{
					backSyncCookieWriter.WriteNextAttributeValue(null);
				}
				else
				{
					byte[] attributeValue3 = this.Watermarks.SerializeToBytes();
					backSyncCookieWriter.WriteNextAttributeValue(attributeValue3);
				}
				backSyncCookieWriter.WriteNextAttributeValue(this.WatermarksInvocationId);
				backSyncCookieWriter.WriteNextAttributeValue(this.PendingWatermarksInvocationId);
				if (this.PendingWatermarks == null)
				{
					backSyncCookieWriter.WriteNextAttributeValue(null);
				}
				else
				{
					byte[] attributeValue4 = this.PendingWatermarks.SerializeToBytes();
					backSyncCookieWriter.WriteNextAttributeValue(attributeValue4);
				}
				backSyncCookieWriter.WriteNextAttributeValue(this.LinkPageStart);
				backSyncCookieWriter.WriteNextAttributeValue(this.LinkPageEnd);
				backSyncCookieWriter.WriteNextAttributeValue(this.LinkRangeStart);
				backSyncCookieWriter.WriteNextAttributeValue(this.ObjectsInLinkPage);
				backSyncCookieWriter.WriteNextAttributeValue(this.AffinityDcFqdn);
				backSyncCookieWriter.WriteNextAttributeValue(this.AffinityTargetDcFqdn);
				backSyncCookieWriter.WriteNextAttributeValue(this.PreSyncLdapPagingCookie);
				result = backSyncCookieWriter.GetBinaryCookie();
			}
			return result;
		}

		// Token: 0x06006599 RID: 26009 RVA: 0x001636B4 File Offset: 0x001618B4
		public void Reset()
		{
			if (this.State >= TenantRelocationSyncState.EnumerateConfigUnitLiveObjects)
			{
				this.State = TenantRelocationSyncState.EnumerateConfigUnitLiveObjects;
				return;
			}
			this.State = TenantRelocationSyncState.PreSyncAllObjects;
		}

		// Token: 0x0600659A RID: 26010 RVA: 0x001636CE File Offset: 0x001618CE
		public void ResetPresyncCookie()
		{
			if (this.IsPreSyncPhase)
			{
				this.PreSyncLdapPagingCookie = null;
			}
		}

		// Token: 0x0600659B RID: 26011 RVA: 0x001636DF File Offset: 0x001618DF
		public void PrepareForFailover()
		{
			this.InvocationId = Guid.Empty;
			this.AffinityDcFqdn = null;
		}

		// Token: 0x0600659C RID: 26012 RVA: 0x001636F4 File Offset: 0x001618F4
		internal static string GetCurrentServerFromSession(IDirectorySession session)
		{
			string text = session.ServerSettings.PreferredGlobalCatalog(session.SessionSettings.GetAccountOrResourceForestFqdn());
			if (string.IsNullOrEmpty(text))
			{
				ADObjectId adobjectId = null;
				PooledLdapConnection pooledLdapConnection = null;
				try
				{
					pooledLdapConnection = session.GetReadConnection(null, ref adobjectId);
					text = pooledLdapConnection.ServerName;
				}
				finally
				{
					if (pooledLdapConnection != null)
					{
						pooledLdapConnection.ReturnToPool();
					}
				}
			}
			return text;
		}

		// Token: 0x0600659D RID: 26013 RVA: 0x00163758 File Offset: 0x00161958
		internal void SetInvocationId(Guid newInvocationId, string dcFqdn)
		{
			if (this.InvocationId == newInvocationId)
			{
				ExTraceGlobals.TenantRelocationTracer.TraceDebug((long)this.TenantConfigUnitObjectGuid.GetHashCode(), "TenantRelocationSyncPageToken.SetInvocationId: new invocationId is the same as the existing one, nothing to do");
				return;
			}
			this.InvocationId = newInvocationId;
			this.AffinityDcFqdn = dcFqdn;
			this.LastReadFailureStartTime = DateTime.MinValue;
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<Guid>((long)this.TenantConfigUnitObjectGuid.GetHashCode(), "TenantRelocationSyncPageToken.SetInvocationId set new invocationId {0}", newInvocationId);
			long num = 0L;
			long num2 = 0L;
			if (this.Watermarks != null)
			{
				this.Watermarks.TryGetValue(this.InvocationId, out num);
			}
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<long>((long)this.TenantConfigUnitObjectGuid.GetHashCode(), "TenantRelocationSyncPageToken.SetInvocationId domain USN for the new invocationId is {0}", num);
			if (this.ConfigNcWatermarks != null)
			{
				this.ConfigNcWatermarks.TryGetValue(this.InvocationId, out num2);
			}
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<long>((long)this.TenantConfigUnitObjectGuid.GetHashCode(), "TenantRelocationSyncPageToken.SetInvocationId config NC USN for the new invocationId is {0}", num2);
			if (this.IsTenantConfigUnitInConfigNc)
			{
				this.ConfigUnitObjectUSN = num2 + 1L;
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<long>((long)this.TenantConfigUnitObjectGuid.GetHashCode(), "TenantRelocationSyncPageToken.SetInvocationId ConfigUnitObjectUSN is set to {0}", this.ConfigUnitObjectUSN);
				this.ConfigUnitTombstoneUSN = num2 + 1L;
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<long>((long)this.TenantConfigUnitObjectGuid.GetHashCode(), "TenantRelocationSyncPageToken.SetInvocationId ConfigUnitTombstoneUSN is set to {0}", this.ConfigUnitTombstoneUSN);
			}
			else
			{
				this.ConfigUnitObjectUSN = num + 1L;
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<long>((long)this.TenantConfigUnitObjectGuid.GetHashCode(), "TenantRelocationSyncPageToken.SetInvocationId ConfigUnitObjectUSN is set to {0}", this.ConfigUnitObjectUSN);
				this.ConfigUnitTombstoneUSN = num + 1L;
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<long>((long)this.TenantConfigUnitObjectGuid.GetHashCode(), "TenantRelocationSyncPageToken.SetInvocationId ConfigUnitTombstoneUSN is set to {0}", this.ConfigUnitTombstoneUSN);
			}
			this.OrganizationalUnitObjectUSN = num + 1L;
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<long>((long)this.TenantConfigUnitObjectGuid.GetHashCode(), "TenantRelocationSyncPageToken.SetInvocationId OrganizationalUnitObjectUSN is set to {0}", this.OrganizationalUnitObjectUSN);
			this.OrganizationalUnitTombstoneUSN = num + 1L;
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<long>((long)this.TenantConfigUnitObjectGuid.GetHashCode(), "TenantRelocationSyncPageToken.SetInvocationId OrganizationalUnitTombstoneUSN is set to {0}", this.OrganizationalUnitTombstoneUSN);
			this.SpecialObjectsUSN = num2 + 1L;
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<long>((long)this.TenantConfigUnitObjectGuid.GetHashCode(), "TenantFullSyncPageToken.SetInvocationId SpecialObjectsUSN is set to {0}", this.SpecialObjectsUSN);
			this.PendingWatermarks = null;
			this.PendingConfigNcWatermarks = null;
			this.PreSyncLdapPagingCookie = null;
			this.Reset();
		}

		// Token: 0x0600659E RID: 26014 RVA: 0x001639F8 File Offset: 0x00161BF8
		public Exception SetErrorState(Exception e)
		{
			DateTime utcNow = DateTime.UtcNow;
			if (this.LastReadFailureStartTime == DateTime.MinValue)
			{
				this.LastReadFailureStartTime = utcNow;
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<DateTime, Guid>((long)this.TenantConfigUnitObjectGuid.GetHashCode(), "SetErrorState: LastReadFailureStartTime set, time:{0}, tenant={1}", utcNow, this.TenantConfigUnitObjectGuid);
				return e;
			}
			if (utcNow.Subtract(this.LastReadFailureStartTime) < TenantRelocationSyncConfiguration.FailoverTimeout)
			{
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<DateTime, DateTime, Guid>((long)this.TenantConfigUnitObjectGuid.GetHashCode(), "SetErrorState: subsequent error set, LastReadFailureStartTime:{0}, time:{1}, tenant={2}", this.LastReadFailureStartTime, DateTime.UtcNow, this.TenantConfigUnitObjectGuid);
				return e;
			}
			string text;
			LocalizedString localizedString;
			if (!string.IsNullOrEmpty(this.AffinityDcFqdn) && !SuitabilityVerifier.IsServerSuitableIgnoreExceptions(this.AffinityDcFqdn, false, null, out text, out localizedString))
			{
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<DateTime, DateTime, Guid>((long)this.TenantConfigUnitObjectGuid.GetHashCode(), "SetErrorState: source failover triggered, LastReadFailureStartTime:{0}, time:{1}, tenant={2}", this.LastReadFailureStartTime, DateTime.UtcNow, this.TenantConfigUnitObjectGuid);
				this.PrepareForFailover();
			}
			if (!string.IsNullOrEmpty(this.AffinityTargetDcFqdn) && !SuitabilityVerifier.IsServerSuitableIgnoreExceptions(this.AffinityTargetDcFqdn, false, null, out text, out localizedString))
			{
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<DateTime, DateTime, Guid>((long)this.TenantConfigUnitObjectGuid.GetHashCode(), "SetErrorState: target failover triggered, LastReadFailureStartTime:{0}, time:{1}, tenant={2}", this.LastReadFailureStartTime, DateTime.UtcNow, this.TenantConfigUnitObjectGuid);
				this.AffinityTargetDcFqdn = null;
			}
			return e;
		}

		// Token: 0x0600659F RID: 26015 RVA: 0x00163B5C File Offset: 0x00161D5C
		internal void SwitchToCompleteState()
		{
			this.CheckLinkPropertiesAreEmpty();
			this.State = TenantRelocationSyncState.Complete;
			this.Watermarks = this.PendingWatermarks;
			this.ConfigNcWatermarks = this.PendingConfigNcWatermarks;
			this.WatermarksInvocationId = this.PendingWatermarksInvocationId;
			long num;
			this.Watermarks.TryGetValue(this.InvocationId, out num);
			long num2;
			this.ConfigNcWatermarks.TryGetValue(this.InvocationId, out num2);
			if (this.OrganizationalUnitObjectUSN <= num)
			{
				this.OrganizationalUnitObjectUSN = num + 1L;
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<long>((long)this.TenantConfigUnitObjectGuid.GetHashCode(), "TenantRelocationSyncPageToken.SwitchToCompleteState OrganizationalUnitObjectUSN is set to {0}", this.OrganizationalUnitObjectUSN);
			}
			if (this.OrganizationalUnitTombstoneUSN <= num)
			{
				this.OrganizationalUnitTombstoneUSN = num + 1L;
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<long>((long)this.TenantConfigUnitObjectGuid.GetHashCode(), "TenantRelocationSyncPageToken.SwitchToCompleteState OrganizationalUnitTombstoneUSN is set to {0}", this.OrganizationalUnitTombstoneUSN);
			}
			if (this.IsTenantConfigUnitInConfigNc)
			{
				if (this.ConfigUnitObjectUSN <= num2)
				{
					this.ConfigUnitObjectUSN = num2 + 1L;
					ExTraceGlobals.TenantRelocationTracer.TraceDebug<long>((long)this.TenantConfigUnitObjectGuid.GetHashCode(), "TenantRelocationSyncPageToken.SwitchToCompleteState ConfigUnitObjectUSN is set to {0}", this.ConfigUnitObjectUSN);
				}
				if (this.ConfigUnitTombstoneUSN <= num2)
				{
					this.ConfigUnitTombstoneUSN = num2 + 1L;
					ExTraceGlobals.TenantRelocationTracer.TraceDebug<long>((long)this.TenantConfigUnitObjectGuid.GetHashCode(), "TenantRelocationSyncPageToken.SwitchToCompleteState ConfigUnitTombstoneUSN is set to {0}", this.ConfigUnitTombstoneUSN);
				}
			}
			else
			{
				if (this.ConfigUnitObjectUSN <= num)
				{
					this.ConfigUnitObjectUSN = num + 1L;
					ExTraceGlobals.TenantRelocationTracer.TraceDebug<long>((long)this.TenantConfigUnitObjectGuid.GetHashCode(), "TenantRelocationSyncPageToken.SwitchToCompleteState ConfigUnitObjectUSN is set to {0}", this.ConfigUnitObjectUSN);
				}
				if (this.ConfigUnitTombstoneUSN <= num)
				{
					this.ConfigUnitTombstoneUSN = num + 1L;
					ExTraceGlobals.TenantRelocationTracer.TraceDebug<long>((long)this.TenantConfigUnitObjectGuid.GetHashCode(), "TenantRelocationSyncPageToken.SwitchToCompleteState ConfigUnitTombstoneUSN is set to {0}", this.ConfigUnitTombstoneUSN);
				}
			}
			if (this.SpecialObjectsUSN <= num2)
			{
				this.SpecialObjectsUSN = num2 + 1L;
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<long>((long)this.TenantConfigUnitObjectGuid.GetHashCode(), "TenantRelocationSyncPageToken.SwitchToCompleteState SpecialObjectsUSN is set to {0}", this.SpecialObjectsUSN);
			}
		}

		// Token: 0x060065A0 RID: 26016 RVA: 0x00163D84 File Offset: 0x00161F84
		internal void SwitchToNextState()
		{
			this.CheckLinkPropertiesAreEmpty();
			switch (this.State)
			{
			case TenantRelocationSyncState.PreSyncAllObjects:
				this.State = TenantRelocationSyncState.EnumerateSpecialObjects;
				return;
			case TenantRelocationSyncState.EnumerateConfigUnitLiveObjects:
			case TenantRelocationSyncState.EnumerateConfigUnitLinksInPage:
				this.State = TenantRelocationSyncState.EnumerateOrganizationalUnitLiveObjects;
				return;
			case TenantRelocationSyncState.EnumerateOrganizationalUnitLiveObjects:
			case TenantRelocationSyncState.EnumerateOrganizationalUnitLinksInPage:
				this.State = TenantRelocationSyncState.EnumerateConfigUnitDeletedObjects;
				return;
			case TenantRelocationSyncState.EnumerateConfigUnitDeletedObjects:
				this.State = TenantRelocationSyncState.EnumerateOrganizationalUnitDeletedObjects;
				return;
			case TenantRelocationSyncState.EnumerateOrganizationalUnitDeletedObjects:
				this.State = TenantRelocationSyncState.EnumerateSpecialObjects;
				return;
			case TenantRelocationSyncState.EnumerateSpecialObjects:
				this.SwitchToCompleteState();
				return;
			default:
				throw new InvalidOperationException("State transition");
			}
		}

		// Token: 0x060065A1 RID: 26017 RVA: 0x00163E00 File Offset: 0x00162000
		private void CheckLinkPropertiesAreEmpty()
		{
			ExTraceGlobals.TenantRelocationTracer.TraceDebug((long)this.TenantConfigUnitObjectGuid.GetHashCode(), "TenantRelocationSyncPageToken.CheckLinkPropertiesAreEmpty entering");
			if (this.PreSyncLdapPagingCookie != null)
			{
				ExTraceGlobals.TenantRelocationTracer.TraceError((long)this.TenantConfigUnitObjectGuid.GetHashCode(), "this.PreSyncLdapPagingCookie != null");
				throw new InvalidOperationException("PreSyncLdapPagingCookie");
			}
		}

		// Token: 0x060065A2 RID: 26018 RVA: 0x00163E68 File Offset: 0x00162068
		internal void SwitchToEnumerateLiveObjectsState()
		{
			if (this.State != TenantRelocationSyncState.EnumerateConfigUnitLinksInPage && this.State != TenantRelocationSyncState.EnumerateOrganizationalUnitLinksInPage)
			{
				throw new InvalidOperationException("State");
			}
			if (this.State == TenantRelocationSyncState.EnumerateConfigUnitLinksInPage)
			{
				this.State = TenantRelocationSyncState.EnumerateConfigUnitLiveObjects;
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<Guid, Guid, long>((long)this.TenantConfigUnitObjectGuid.GetHashCode(), "Starting enumeration of live objects for {0} on {1} from USN {2}", this.TenantConfigUnitObjectGuid, this.InvocationId, this.ConfigUnitObjectUSN);
				return;
			}
			this.State = TenantRelocationSyncState.EnumerateOrganizationalUnitLiveObjects;
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<Guid, Guid, long>((long)this.TenantConfigUnitObjectGuid.GetHashCode(), "Starting enumeration of live objects for {0} on {1} from USN {2}", this.TenantConfigUnitObjectGuid, this.InvocationId, this.OrganizationalUnitObjectUSN);
		}

		// Token: 0x04004341 RID: 17217
		internal const int CurrentVersion = 1;

		// Token: 0x04004342 RID: 17218
		internal static BackSyncCookieAttribute[] TenantRelocationSyncPageTokenAttributeSchema_Version_1 = new BackSyncCookieAttribute[]
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
				Name = "TenantConfigUnitObjectGuid",
				DataType = typeof(Guid),
				DefaultValue = Guid.Empty
			},
			new BackSyncCookieAttribute
			{
				Name = "TenantOrganizationalUnitObjectGuid",
				DataType = typeof(Guid),
				DefaultValue = Guid.Empty
			},
			new BackSyncCookieAttribute
			{
				Name = "IsTenantConfigUnitInConfigNc",
				DataType = typeof(bool),
				DefaultValue = true
			},
			new BackSyncCookieAttribute
			{
				Name = "PartitionHint",
				DataType = typeof(byte[]),
				DefaultValue = null
			},
			new BackSyncCookieAttribute
			{
				Name = "TenantRelocationSyncState",
				DataType = typeof(int),
				DefaultValue = 0
			},
			new BackSyncCookieAttribute
			{
				Name = "ConfigUnitObjectUSN",
				DataType = typeof(long),
				DefaultValue = Convert.ToInt64(0)
			},
			new BackSyncCookieAttribute
			{
				Name = "ConfigUnitTombstoneUSN",
				DataType = typeof(long),
				DefaultValue = Convert.ToInt64(0)
			},
			new BackSyncCookieAttribute
			{
				Name = "OrganizationalUnitObjectUSN",
				DataType = typeof(long),
				DefaultValue = Convert.ToInt64(0)
			},
			new BackSyncCookieAttribute
			{
				Name = "OrganizationalUnitTombstoneUSN",
				DataType = typeof(long),
				DefaultValue = Convert.ToInt64(0)
			},
			new BackSyncCookieAttribute
			{
				Name = "SpecialObjectsUSN",
				DataType = typeof(long),
				DefaultValue = Convert.ToInt64(0)
			},
			new BackSyncCookieAttribute
			{
				Name = "ConfigNcWaterMarks",
				DataType = typeof(byte[]),
				DefaultValue = null
			},
			new BackSyncCookieAttribute
			{
				Name = "PendingConfigNcWaterMarks",
				DataType = typeof(byte[]),
				DefaultValue = null
			},
			new BackSyncCookieAttribute
			{
				Name = "Watermarks",
				DataType = typeof(byte[]),
				DefaultValue = null
			},
			new BackSyncCookieAttribute
			{
				Name = "WatermarksInvocationId",
				DataType = typeof(Guid),
				DefaultValue = Guid.Empty
			},
			new BackSyncCookieAttribute
			{
				Name = "PendingWatermarksInvocationId",
				DataType = typeof(Guid),
				DefaultValue = Guid.Empty
			},
			new BackSyncCookieAttribute
			{
				Name = "PendingWatermarks",
				DataType = typeof(byte[]),
				DefaultValue = null
			},
			new BackSyncCookieAttribute
			{
				Name = "LinkPageStart",
				DataType = typeof(long),
				DefaultValue = Convert.ToInt64(0)
			},
			new BackSyncCookieAttribute
			{
				Name = "LinkPageEnd",
				DataType = typeof(long),
				DefaultValue = Convert.ToInt64(0)
			},
			new BackSyncCookieAttribute
			{
				Name = "LinkRangeStart",
				DataType = typeof(int),
				DefaultValue = 0
			},
			new BackSyncCookieAttribute
			{
				Name = "ObjectsInLinkPage",
				DataType = typeof(int),
				DefaultValue = 0
			},
			new BackSyncCookieAttribute
			{
				Name = "AffinityDcFqdn",
				DataType = typeof(string),
				DefaultValue = string.Empty
			},
			new BackSyncCookieAttribute
			{
				Name = "AffinityTargetDcFqdn",
				DataType = typeof(string),
				DefaultValue = string.Empty
			},
			new BackSyncCookieAttribute
			{
				Name = "PreSyncLdapPagingCookie",
				DataType = typeof(byte[]),
				DefaultValue = null
			}
		};

		// Token: 0x04004343 RID: 17219
		internal static BackSyncCookieAttribute[][] TenantRelocationSyncPageTokenAttributeSchemaByVersions = new BackSyncCookieAttribute[][]
		{
			BackSyncCookieAttribute.BackSyncCookieVersionSchema,
			TenantRelocationSyncPageToken.TenantRelocationSyncPageTokenAttributeSchema_Version_1
		};
	}
}
