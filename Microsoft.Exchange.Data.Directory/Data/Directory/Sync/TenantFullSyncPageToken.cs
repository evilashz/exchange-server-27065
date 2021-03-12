using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data.Directory.DirSync;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.BackSync;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020007C7 RID: 1991
	internal class TenantFullSyncPageToken : IFullSyncPageToken, ISyncCookie
	{
		// Token: 0x060062CB RID: 25291 RVA: 0x00155438 File Offset: 0x00153638
		public TenantFullSyncPageToken(Guid invocationId, Guid tenantExternalDirectoryId, ADObjectId tenantOuId, ServiceInstanceId serviceInstanceId, bool useDirSyncBasedTfs = false)
		{
			ExTraceGlobals.TenantFullSyncTracer.TraceDebug((long)this.TenantExternalDirectoryId.GetHashCode(), "New TenantFullSyncPageToken");
			this.Version = 3;
			ExTraceGlobals.TenantFullSyncTracer.TraceDebug<int>((long)this.TenantExternalDirectoryId.GetHashCode(), "TenantFullSyncPageToken this.Version = {0}", this.Version);
			this.TenantExternalDirectoryId = tenantExternalDirectoryId;
			ExTraceGlobals.TenantFullSyncTracer.TraceDebug<Guid>((long)this.TenantExternalDirectoryId.GetHashCode(), "TenantFullSyncPageToken this.TenantExternalDirectoryId = {0}", this.TenantExternalDirectoryId);
			this.TenantObjectGuid = tenantOuId.ObjectGuid;
			ExTraceGlobals.TenantFullSyncTracer.TraceDebug<Guid>((long)this.TenantExternalDirectoryId.GetHashCode(), "TenantFullSyncPageToken this.TenantObjectGuid = {0}", this.TenantObjectGuid);
			this.State = TenantFullSyncState.EnumerateLiveObjects;
			ExTraceGlobals.TenantFullSyncTracer.TraceDebug<string>((long)this.TenantExternalDirectoryId.GetHashCode(), "TenantFullSyncPageToken this.State = {0}", this.State.ToString());
			this.ServiceInstanceId = serviceInstanceId;
			ExTraceGlobals.TenantFullSyncTracer.TraceDebug<ServiceInstanceId>((long)this.TenantExternalDirectoryId.GetHashCode(), "TenantFullSyncPageToken this.ServiceInstanceId = {0}", this.ServiceInstanceId);
			this.ErrorObjectsAndFailureCounts = new Dictionary<string, int>();
			this.SequenceId = Guid.NewGuid();
			this.SequenceStartTimestamp = DateTime.UtcNow;
			ExTraceGlobals.TenantFullSyncTracer.TraceDebug<Guid, DateTime>((long)this.TenantExternalDirectoryId.GetHashCode(), "TenantFullSyncPageToken Starting a new sequence this.SequenceId = {0} this.SequenceStartTimestamp = {1} ", this.SequenceId, this.SequenceStartTimestamp);
			this.TenantScopedBackSyncCookie = (useDirSyncBasedTfs ? new BackSyncCookie(this.ServiceInstanceId) : null);
			this.InvocationId = (useDirSyncBasedTfs ? this.TenantScopedBackSyncCookie.InvocationId : invocationId);
			ExTraceGlobals.TenantFullSyncTracer.TraceDebug<Guid>((long)this.TenantExternalDirectoryId.GetHashCode(), "TenantFullSyncPageToken this.InvocationId = {0}", this.InvocationId);
			this.UseContainerizedUsnChangedIndex = false;
			if (SyncConfiguration.EnableContainerizedUsnChangedOptimization())
			{
				Guid preferredDCWithContainerizedUsnChanged = SyncConfiguration.GetPreferredDCWithContainerizedUsnChanged(this.ServiceInstanceId.InstanceId);
				if (preferredDCWithContainerizedUsnChanged != Guid.Empty)
				{
					this.InvocationId = preferredDCWithContainerizedUsnChanged;
					this.UseContainerizedUsnChangedIndex = true;
					ExTraceGlobals.TenantFullSyncTracer.TraceDebug<Guid>((long)this.TenantExternalDirectoryId.GetHashCode(), "TenantFullSyncPageToken overwriting this.InvocationId = {0} and this.UseContainerizedUsnChangedIndex = true", this.InvocationId);
				}
				else
				{
					ExTraceGlobals.TenantFullSyncTracer.TraceDebug<ServiceInstanceId>((long)this.TenantExternalDirectoryId.GetHashCode(), "TenantFullSyncPageToken: Could not find preferred DC for service instance {0}. Containerized USN index will NOT be used.", this.ServiceInstanceId);
				}
			}
			if (this.UseContainerizedUsnChangedIndex && useDirSyncBasedTfs)
			{
				throw new InvalidOperationException("Invalid configuration - cannot use Containerized UsnChanged Index and Dirsync based TFS simultaneously.");
			}
			if (this.UseContainerizedUsnChangedIndex)
			{
				this.PreviousState = this.State;
			}
		}

		// Token: 0x1700231E RID: 8990
		// (get) Token: 0x060062CC RID: 25292 RVA: 0x001556E9 File Offset: 0x001538E9
		// (set) Token: 0x060062CD RID: 25293 RVA: 0x001556F1 File Offset: 0x001538F1
		public int Version { get; private set; }

		// Token: 0x1700231F RID: 8991
		// (get) Token: 0x060062CE RID: 25294 RVA: 0x001556FA File Offset: 0x001538FA
		// (set) Token: 0x060062CF RID: 25295 RVA: 0x00155702 File Offset: 0x00153902
		public Guid TenantExternalDirectoryId { get; private set; }

		// Token: 0x17002320 RID: 8992
		// (get) Token: 0x060062D0 RID: 25296 RVA: 0x0015570B File Offset: 0x0015390B
		// (set) Token: 0x060062D1 RID: 25297 RVA: 0x00155713 File Offset: 0x00153913
		public Guid TenantObjectGuid { get; private set; }

		// Token: 0x17002321 RID: 8993
		// (get) Token: 0x060062D2 RID: 25298 RVA: 0x0015571C File Offset: 0x0015391C
		// (set) Token: 0x060062D3 RID: 25299 RVA: 0x00155724 File Offset: 0x00153924
		public Guid InvocationId { get; internal set; }

		// Token: 0x17002322 RID: 8994
		// (get) Token: 0x060062D4 RID: 25300 RVA: 0x0015572D File Offset: 0x0015392D
		public bool ReadyToMerge
		{
			get
			{
				return this.State == TenantFullSyncState.Complete;
			}
		}

		// Token: 0x17002323 RID: 8995
		// (get) Token: 0x060062D5 RID: 25301 RVA: 0x00155738 File Offset: 0x00153938
		// (set) Token: 0x060062D6 RID: 25302 RVA: 0x00155740 File Offset: 0x00153940
		public TenantFullSyncState State { get; private set; }

		// Token: 0x17002324 RID: 8996
		// (get) Token: 0x060062D7 RID: 25303 RVA: 0x00155749 File Offset: 0x00153949
		// (set) Token: 0x060062D8 RID: 25304 RVA: 0x00155751 File Offset: 0x00153951
		public Dictionary<string, int> ErrorObjectsAndFailureCounts { get; protected set; }

		// Token: 0x17002325 RID: 8997
		// (get) Token: 0x060062D9 RID: 25305 RVA: 0x0015575A File Offset: 0x0015395A
		// (set) Token: 0x060062DA RID: 25306 RVA: 0x00155762 File Offset: 0x00153962
		internal long ObjectUpdateSequenceNumber { get; set; }

		// Token: 0x17002326 RID: 8998
		// (get) Token: 0x060062DB RID: 25307 RVA: 0x0015576B File Offset: 0x0015396B
		// (set) Token: 0x060062DC RID: 25308 RVA: 0x00155773 File Offset: 0x00153973
		internal long TombstoneUpdateSequenceNumber { get; set; }

		// Token: 0x17002327 RID: 8999
		// (get) Token: 0x060062DD RID: 25309 RVA: 0x0015577C File Offset: 0x0015397C
		// (set) Token: 0x060062DE RID: 25310 RVA: 0x00155784 File Offset: 0x00153984
		internal long LinkPageStart { get; private set; }

		// Token: 0x17002328 RID: 9000
		// (get) Token: 0x060062DF RID: 25311 RVA: 0x0015578D File Offset: 0x0015398D
		// (set) Token: 0x060062E0 RID: 25312 RVA: 0x00155795 File Offset: 0x00153995
		internal long LinkPageEnd { get; private set; }

		// Token: 0x17002329 RID: 9001
		// (get) Token: 0x060062E1 RID: 25313 RVA: 0x0015579E File Offset: 0x0015399E
		// (set) Token: 0x060062E2 RID: 25314 RVA: 0x001557A6 File Offset: 0x001539A6
		internal int LinkRangeStart { get; private set; }

		// Token: 0x1700232A RID: 9002
		// (get) Token: 0x060062E3 RID: 25315 RVA: 0x001557AF File Offset: 0x001539AF
		// (set) Token: 0x060062E4 RID: 25316 RVA: 0x001557B7 File Offset: 0x001539B7
		internal int ObjectsInLinkPage { get; private set; }

		// Token: 0x1700232B RID: 9003
		// (get) Token: 0x060062E5 RID: 25317 RVA: 0x001557C0 File Offset: 0x001539C0
		// (set) Token: 0x060062E6 RID: 25318 RVA: 0x001557C8 File Offset: 0x001539C8
		internal WatermarkMap Watermarks { get; set; }

		// Token: 0x1700232C RID: 9004
		// (get) Token: 0x060062E7 RID: 25319 RVA: 0x001557D1 File Offset: 0x001539D1
		// (set) Token: 0x060062E8 RID: 25320 RVA: 0x001557D9 File Offset: 0x001539D9
		internal WatermarkMap PendingWatermarks { get; set; }

		// Token: 0x1700232D RID: 9005
		// (get) Token: 0x060062E9 RID: 25321 RVA: 0x001557E2 File Offset: 0x001539E2
		// (set) Token: 0x060062EA RID: 25322 RVA: 0x001557EA File Offset: 0x001539EA
		internal Guid WatermarksInvocationId { get; set; }

		// Token: 0x1700232E RID: 9006
		// (get) Token: 0x060062EB RID: 25323 RVA: 0x001557F3 File Offset: 0x001539F3
		// (set) Token: 0x060062EC RID: 25324 RVA: 0x001557FB File Offset: 0x001539FB
		internal Guid PendingWatermarksInvocationId { get; set; }

		// Token: 0x1700232F RID: 9007
		// (get) Token: 0x060062ED RID: 25325 RVA: 0x00155804 File Offset: 0x00153A04
		// (set) Token: 0x060062EE RID: 25326 RVA: 0x0015580C File Offset: 0x00153A0C
		internal ServiceInstanceId ServiceInstanceId { get; private set; }

		// Token: 0x17002330 RID: 9008
		// (get) Token: 0x060062EF RID: 25327 RVA: 0x00155815 File Offset: 0x00153A15
		// (set) Token: 0x060062F0 RID: 25328 RVA: 0x0015581D File Offset: 0x00153A1D
		public DateTime SequenceStartTimestamp { get; private set; }

		// Token: 0x17002331 RID: 9009
		// (get) Token: 0x060062F1 RID: 25329 RVA: 0x00155826 File Offset: 0x00153A26
		// (set) Token: 0x060062F2 RID: 25330 RVA: 0x0015582E File Offset: 0x00153A2E
		public Guid SequenceId { get; private set; }

		// Token: 0x17002332 RID: 9010
		// (get) Token: 0x060062F3 RID: 25331 RVA: 0x00155837 File Offset: 0x00153A37
		// (set) Token: 0x060062F4 RID: 25332 RVA: 0x0015583F File Offset: 0x00153A3F
		public BackSyncCookie TenantScopedBackSyncCookie { get; set; }

		// Token: 0x17002333 RID: 9011
		// (get) Token: 0x060062F5 RID: 25333 RVA: 0x00155848 File Offset: 0x00153A48
		// (set) Token: 0x060062F6 RID: 25334 RVA: 0x00155850 File Offset: 0x00153A50
		internal bool UseContainerizedUsnChangedIndex { get; set; }

		// Token: 0x17002334 RID: 9012
		// (get) Token: 0x060062F7 RID: 25335 RVA: 0x00155859 File Offset: 0x00153A59
		// (set) Token: 0x060062F8 RID: 25336 RVA: 0x00155861 File Offset: 0x00153A61
		internal long SoftDeletedObjectUpdateSequenceNumber { get; set; }

		// Token: 0x17002335 RID: 9013
		// (get) Token: 0x060062F9 RID: 25337 RVA: 0x0015586A File Offset: 0x00153A6A
		// (set) Token: 0x060062FA RID: 25338 RVA: 0x00155872 File Offset: 0x00153A72
		public TenantFullSyncState PreviousState { get; private set; }

		// Token: 0x060062FB RID: 25339 RVA: 0x0015587B File Offset: 0x00153A7B
		internal static TenantFullSyncPageToken Parse(byte[] tokenBytes)
		{
			if (tokenBytes == null)
			{
				throw new ArgumentNullException("tokenBytes");
			}
			return new TenantFullSyncPageToken(tokenBytes);
		}

		// Token: 0x060062FC RID: 25340 RVA: 0x00155894 File Offset: 0x00153A94
		protected TenantFullSyncPageToken(byte[] tokenBytes)
		{
			Exception ex = null;
			try
			{
				using (BackSyncCookieReader backSyncCookieReader = BackSyncCookieReader.Create(tokenBytes, typeof(TenantFullSyncPageToken)))
				{
					this.Version = (int)backSyncCookieReader.GetNextAttributeValue();
					this.ServiceInstanceId = new ServiceInstanceId((string)backSyncCookieReader.GetNextAttributeValue());
					this.Timestamp = DateTime.FromBinary((long)backSyncCookieReader.GetNextAttributeValue());
					this.LastReadFailureStartTime = DateTime.FromBinary((long)backSyncCookieReader.GetNextAttributeValue());
					this.InvocationId = (Guid)backSyncCookieReader.GetNextAttributeValue();
					this.TenantExternalDirectoryId = (Guid)backSyncCookieReader.GetNextAttributeValue();
					this.TenantObjectGuid = (Guid)backSyncCookieReader.GetNextAttributeValue();
					this.State = (TenantFullSyncState)backSyncCookieReader.GetNextAttributeValue();
					this.ObjectUpdateSequenceNumber = (long)backSyncCookieReader.GetNextAttributeValue();
					this.TombstoneUpdateSequenceNumber = (long)backSyncCookieReader.GetNextAttributeValue();
					byte[] array = (byte[])backSyncCookieReader.GetNextAttributeValue();
					if (array != null)
					{
						this.PendingWatermarks = WatermarkMap.Parse(array);
					}
					this.PendingWatermarksInvocationId = (Guid)backSyncCookieReader.GetNextAttributeValue();
					byte[] array2 = (byte[])backSyncCookieReader.GetNextAttributeValue();
					if (array2 != null)
					{
						this.Watermarks = WatermarkMap.Parse(array2);
					}
					this.WatermarksInvocationId = (Guid)backSyncCookieReader.GetNextAttributeValue();
					this.LinkPageStart = (long)backSyncCookieReader.GetNextAttributeValue();
					this.LinkPageEnd = (long)backSyncCookieReader.GetNextAttributeValue();
					this.LinkRangeStart = (int)backSyncCookieReader.GetNextAttributeValue();
					this.ObjectsInLinkPage = (int)backSyncCookieReader.GetNextAttributeValue();
					string[] array3 = (string[])backSyncCookieReader.GetNextAttributeValue();
					this.ErrorObjectsAndFailureCounts = ((array3 != null) ? BackSyncCookie.ParseErrorObjectsAndFailureCounts(array3) : new Dictionary<string, int>());
					this.SequenceStartTimestamp = DateTime.FromBinary((long)backSyncCookieReader.GetNextAttributeValue());
					this.SequenceId = (Guid)backSyncCookieReader.GetNextAttributeValue();
					byte[] array4 = (byte[])backSyncCookieReader.GetNextAttributeValue();
					if (array4 != null)
					{
						this.TenantScopedBackSyncCookie = BackSyncCookie.Parse(array4);
						this.InvocationId = this.TenantScopedBackSyncCookie.InvocationId;
					}
					this.UseContainerizedUsnChangedIndex = (bool)backSyncCookieReader.GetNextAttributeValue();
					this.SoftDeletedObjectUpdateSequenceNumber = (long)backSyncCookieReader.GetNextAttributeValue();
					this.PreviousState = (TenantFullSyncState)backSyncCookieReader.GetNextAttributeValue();
				}
			}
			catch (ArgumentException ex2)
			{
				ExTraceGlobals.TenantFullSyncTracer.TraceError<string>((long)this.TenantExternalDirectoryId.GetHashCode(), "TenantFullSyncPageToken ArgumentException {0}", ex2.ToString());
				ex = ex2;
			}
			catch (IOException ex3)
			{
				ExTraceGlobals.TenantFullSyncTracer.TraceError<string>((long)this.TenantExternalDirectoryId.GetHashCode(), "TenantFullSyncPageToken IOException {0}", ex3.ToString());
				ex = ex3;
			}
			catch (FormatException ex4)
			{
				ExTraceGlobals.TenantFullSyncTracer.TraceError<string>((long)this.TenantExternalDirectoryId.GetHashCode(), "TenantFullSyncPageToken FormatException {0}", ex4.ToString());
				ex = ex4;
			}
			catch (InvalidCookieException ex5)
			{
				ExTraceGlobals.TenantFullSyncTracer.TraceError<string>((long)this.TenantExternalDirectoryId.GetHashCode(), "TenantFullSyncPageToken InvalidCookieException {0}", ex5.ToString());
				ex = ex5;
			}
			if (ex != null)
			{
				ExTraceGlobals.TenantFullSyncTracer.TraceError<string>((long)this.TenantExternalDirectoryId.GetHashCode(), "TenantFullSyncPageToken throw InvalidCookieException {0}", ex.ToString());
				throw new InvalidCookieException(ex);
			}
		}

		// Token: 0x17002336 RID: 9014
		// (get) Token: 0x060062FD RID: 25341 RVA: 0x00155C50 File Offset: 0x00153E50
		public BackSyncOptions SyncOptions
		{
			get
			{
				return BackSyncOptions.IncludeLinks;
			}
		}

		// Token: 0x17002337 RID: 9015
		// (get) Token: 0x060062FE RID: 25342 RVA: 0x00155C53 File Offset: 0x00153E53
		public bool MoreData
		{
			get
			{
				return this.State != TenantFullSyncState.Complete;
			}
		}

		// Token: 0x17002338 RID: 9016
		// (get) Token: 0x060062FF RID: 25343 RVA: 0x00155C61 File Offset: 0x00153E61
		// (set) Token: 0x06006300 RID: 25344 RVA: 0x00155C69 File Offset: 0x00153E69
		public DateTime Timestamp { get; set; }

		// Token: 0x17002339 RID: 9017
		// (get) Token: 0x06006301 RID: 25345 RVA: 0x00155C72 File Offset: 0x00153E72
		// (set) Token: 0x06006302 RID: 25346 RVA: 0x00155C7A File Offset: 0x00153E7A
		public DateTime LastReadFailureStartTime { get; set; }

		// Token: 0x06006303 RID: 25347 RVA: 0x00155C84 File Offset: 0x00153E84
		public virtual byte[] ToByteArray()
		{
			byte[] result = null;
			using (BackSyncCookieWriter backSyncCookieWriter = BackSyncCookieWriter.Create(typeof(TenantFullSyncPageToken)))
			{
				backSyncCookieWriter.WriteNextAttributeValue(this.Version);
				backSyncCookieWriter.WriteNextAttributeValue(this.ServiceInstanceId.InstanceId);
				backSyncCookieWriter.WriteNextAttributeValue(this.Timestamp.ToBinary());
				backSyncCookieWriter.WriteNextAttributeValue(this.LastReadFailureStartTime.ToBinary());
				backSyncCookieWriter.WriteNextAttributeValue(this.InvocationId);
				backSyncCookieWriter.WriteNextAttributeValue(this.TenantExternalDirectoryId);
				backSyncCookieWriter.WriteNextAttributeValue(this.TenantObjectGuid);
				backSyncCookieWriter.WriteNextAttributeValue((int)this.State);
				backSyncCookieWriter.WriteNextAttributeValue(this.ObjectUpdateSequenceNumber);
				backSyncCookieWriter.WriteNextAttributeValue(this.TombstoneUpdateSequenceNumber);
				if (this.PendingWatermarks == null)
				{
					backSyncCookieWriter.WriteNextAttributeValue(null);
				}
				else
				{
					byte[] attributeValue = this.PendingWatermarks.SerializeToBytes();
					backSyncCookieWriter.WriteNextAttributeValue(attributeValue);
				}
				backSyncCookieWriter.WriteNextAttributeValue(this.PendingWatermarksInvocationId);
				if (this.Watermarks == null)
				{
					backSyncCookieWriter.WriteNextAttributeValue(null);
				}
				else
				{
					byte[] attributeValue2 = this.Watermarks.SerializeToBytes();
					backSyncCookieWriter.WriteNextAttributeValue(attributeValue2);
				}
				backSyncCookieWriter.WriteNextAttributeValue(this.WatermarksInvocationId);
				backSyncCookieWriter.WriteNextAttributeValue(this.LinkPageStart);
				backSyncCookieWriter.WriteNextAttributeValue(this.LinkPageEnd);
				backSyncCookieWriter.WriteNextAttributeValue(this.LinkRangeStart);
				backSyncCookieWriter.WriteNextAttributeValue(this.ObjectsInLinkPage);
				string[] attributeValue3 = BackSyncCookie.ConvertErrorObjectsAndFailureCountsToArray(this.ErrorObjectsAndFailureCounts);
				backSyncCookieWriter.WriteNextAttributeValue(attributeValue3);
				backSyncCookieWriter.WriteNextAttributeValue(this.SequenceStartTimestamp.ToBinary());
				backSyncCookieWriter.WriteNextAttributeValue(this.SequenceId);
				if (this.TenantScopedBackSyncCookie == null)
				{
					backSyncCookieWriter.WriteNextAttributeValue(null);
				}
				else
				{
					byte[] attributeValue4 = this.TenantScopedBackSyncCookie.ToByteArray();
					backSyncCookieWriter.WriteNextAttributeValue(attributeValue4);
				}
				backSyncCookieWriter.WriteNextAttributeValue(this.UseContainerizedUsnChangedIndex);
				backSyncCookieWriter.WriteNextAttributeValue(this.SoftDeletedObjectUpdateSequenceNumber);
				backSyncCookieWriter.WriteNextAttributeValue((int)this.PreviousState);
				result = backSyncCookieWriter.GetBinaryCookie();
			}
			return result;
		}

		// Token: 0x06006304 RID: 25348 RVA: 0x00155ED8 File Offset: 0x001540D8
		public void PrepareForFailover()
		{
			ExTraceGlobals.TenantFullSyncTracer.TraceDebug((long)this.TenantExternalDirectoryId.GetHashCode(), "TenantFullSyncPageToken.PrepareForFailover entering");
			long num = 0L;
			if (this.Watermarks != null)
			{
				ExTraceGlobals.TenantFullSyncTracer.TraceDebug<Guid>((long)this.TenantExternalDirectoryId.GetHashCode(), "Get USN from DSA invocation id {0}", this.InvocationId);
				this.Watermarks.TryGetValue(this.InvocationId, out num);
				ExTraceGlobals.TenantFullSyncTracer.TraceDebug<long>((long)this.TenantExternalDirectoryId.GetHashCode(), "TenantFullSyncPageToken.PrepareForFailover usnFromCurrentDc = {0}", num);
			}
			if (this.ObjectUpdateSequenceNumber <= num + 1L && this.TombstoneUpdateSequenceNumber <= num + 1L)
			{
				this.InvocationId = Guid.Empty;
				ExTraceGlobals.ActiveDirectoryTracer.TraceWarning<string, Guid, Guid>((long)this.TenantExternalDirectoryId.GetHashCode(), "Allowing failover for {0} (Tenant={1}) from {2}. New DC will be picked on the next request.", base.GetType().Name, this.TenantExternalDirectoryId, this.InvocationId);
				return;
			}
			ExTraceGlobals.ActiveDirectoryTracer.TraceWarning<string, Guid, Guid>((long)this.TenantExternalDirectoryId.GetHashCode(), "NOT allowing failover for {0} (Tenant={1}) from {2} because some data have already been read.", base.GetType().Name, this.TenantExternalDirectoryId, this.InvocationId);
		}

		// Token: 0x06006305 RID: 25349 RVA: 0x00156014 File Offset: 0x00154214
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

		// Token: 0x06006306 RID: 25350 RVA: 0x00156078 File Offset: 0x00154278
		internal virtual Guid SelectDomainController(PartitionId partitionId)
		{
			ExTraceGlobals.TenantFullSyncTracer.TraceDebug((long)this.TenantExternalDirectoryId.GetHashCode(), "TenantFullSyncPageToken.SelectDomainController entering");
			if (this.InvocationId != Guid.Empty)
			{
				ExTraceGlobals.TenantFullSyncTracer.TraceError<Guid>((long)this.TenantExternalDirectoryId.GetHashCode(), "InvocationId {0} already set", this.InvocationId);
				throw new InvalidOperationException("InvocationId");
			}
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromAccountPartitionRootOrgScopeSet(partitionId), 795, "SelectDomainController", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\Sync\\BackSync\\TenantFullSyncPageToken.cs");
			string currentServerFromSession = TenantFullSyncPageToken.GetCurrentServerFromSession(topologyConfigurationSession);
			ExTraceGlobals.TenantFullSyncTracer.TraceDebug<string>((long)this.TenantExternalDirectoryId.GetHashCode(), "TenantFullSyncPageToken.SelectDomainController dcName {0}", currentServerFromSession);
			this.InvocationId = topologyConfigurationSession.GetInvocationIdByFqdn(currentServerFromSession);
			ExTraceGlobals.ActiveDirectoryTracer.TraceInformation<Guid, string, Guid>(10429, (long)this.TenantExternalDirectoryId.GetHashCode(), "Randomly picked DC {0} for {1} (Tenant={2}).", this.InvocationId, base.GetType().Name, this.TenantExternalDirectoryId);
			return this.InvocationId;
		}

		// Token: 0x06006307 RID: 25351 RVA: 0x00156194 File Offset: 0x00154394
		internal void SwitchToEnumerateDeletedObjectsState()
		{
			ExTraceGlobals.TenantFullSyncTracer.TraceDebug((long)this.TenantExternalDirectoryId.GetHashCode(), "TenantFullSyncPageToken.SwitchToEnumerateDeletedObjectsState entering");
			ExTraceGlobals.TenantFullSyncTracer.TraceDebug<TenantFullSyncState>((long)this.TenantExternalDirectoryId.GetHashCode(), "TenantFullSyncPageToken.SwitchToEnumerateDeletedObjectsState this.State = {0}", this.State);
			if (this.State == TenantFullSyncState.EnumerateDeletedObjects)
			{
				ExTraceGlobals.TenantFullSyncTracer.TraceError<string>((long)this.TenantExternalDirectoryId.GetHashCode(), "Invalid state {0} to SwitchToEnumerateDeletedObjectsState", this.State.ToString());
				throw new InvalidOperationException("State");
			}
			this.CheckLinkPropertiesAreEmpty();
			this.State = TenantFullSyncState.EnumerateDeletedObjects;
			ExTraceGlobals.TenantFullSyncTracer.TraceDebug<Guid, Guid, long>((long)this.TenantExternalDirectoryId.GetHashCode(), "Starting enumeration of deleted objects for {0} on {1} from USN {2}", this.TenantExternalDirectoryId, this.InvocationId, this.TombstoneUpdateSequenceNumber);
		}

		// Token: 0x06006308 RID: 25352 RVA: 0x0015627C File Offset: 0x0015447C
		internal void SwitchToEnumerateLiveObjectsState()
		{
			if (this.ObjectsInLinkPage == 0)
			{
				throw new InvalidOperationException("ObjectsInLinkPage");
			}
			if (this.State != TenantFullSyncState.EnumerateLinksInPage && this.State != TenantFullSyncState.Complete)
			{
				throw new InvalidOperationException("State");
			}
			this.State = TenantFullSyncState.EnumerateLiveObjects;
			this.LinkPageStart = 0L;
			this.LinkPageEnd = 0L;
			this.LinkRangeStart = 0;
			this.ObjectsInLinkPage = 0;
			ExTraceGlobals.TenantFullSyncTracer.TraceDebug<Guid, Guid, long>((long)this.TenantExternalDirectoryId.GetHashCode(), "Starting enumeration of live objects for {0} on {1} from USN {2}", this.TenantExternalDirectoryId, this.InvocationId, this.ObjectUpdateSequenceNumber);
		}

		// Token: 0x06006309 RID: 25353 RVA: 0x00156314 File Offset: 0x00154514
		internal void SwitchToEnumerateSoftDeletedObjectsState()
		{
			if (this.State != TenantFullSyncState.EnumerateLinksInPage && this.State != TenantFullSyncState.EnumerateLiveObjects)
			{
				throw new InvalidOperationException("State");
			}
			this.PreviousState = this.State;
			this.State = TenantFullSyncState.EnumerateSoftDeletedObjects;
			this.LinkPageStart = 0L;
			this.LinkPageEnd = 0L;
			this.LinkRangeStart = 0;
			this.ObjectsInLinkPage = 0;
			ExTraceGlobals.TenantFullSyncTracer.TraceDebug<Guid, Guid, long>((long)this.TenantExternalDirectoryId.GetHashCode(), "Starting enumeration of soft-deleted objects for {0} on {1} from USN {2}", this.TenantExternalDirectoryId, this.InvocationId, this.SoftDeletedObjectUpdateSequenceNumber);
		}

		// Token: 0x0600630A RID: 25354 RVA: 0x001563A4 File Offset: 0x001545A4
		internal void SwitchToEnumerateLinksState(long linkPageStart, long linkPageEnd, int objectsInLinkPage)
		{
			if (this.State != TenantFullSyncState.EnumerateLiveObjects && this.State != TenantFullSyncState.EnumerateSoftDeletedObjects)
			{
				ExTraceGlobals.TenantFullSyncTracer.TraceError((long)this.TenantExternalDirectoryId.GetHashCode(), "this.State != EnumerateLiveObjects and EnumerateSoftDeletedObjects");
				throw new InvalidOperationException("State");
			}
			this.PreviousState = this.State;
			this.CheckLinkPropertiesAreEmpty();
			this.SetEnumerateLinksParams(linkPageStart, linkPageEnd, FullSyncConfiguration.InitialLinkReadSize, objectsInLinkPage, this.State);
			this.State = TenantFullSyncState.EnumerateLinksInPage;
			ExTraceGlobals.TenantFullSyncTracer.TraceDebug((long)this.TenantExternalDirectoryId.GetHashCode(), "Starting enumeration of links for {0} on {1} in USN range {2}-{3} (total count {4})", new object[]
			{
				this.TenantExternalDirectoryId,
				this.InvocationId,
				this.LinkPageStart,
				this.LinkPageEnd,
				this.ObjectsInLinkPage
			});
		}

		// Token: 0x0600630B RID: 25355 RVA: 0x00156490 File Offset: 0x00154690
		internal void UpdateEnumerateLinksState(long linkPageStart, long linkPageEnd, int linkRangeStart, int objectsInLinkPage)
		{
			this.SetEnumerateLinksParams(linkPageStart, linkPageEnd, linkRangeStart, objectsInLinkPage, TenantFullSyncState.EnumerateLinksInPage);
			ExTraceGlobals.TenantFullSyncTracer.TraceDebug((long)this.TenantExternalDirectoryId.GetHashCode(), "Setting link range for {0} on {1} to USN range {2}-{3} (total count {4}), values start at {5}", new object[]
			{
				this.TenantExternalDirectoryId,
				this.InvocationId,
				this.LinkPageStart,
				this.LinkPageEnd,
				this.ObjectsInLinkPage,
				this.LinkRangeStart
			});
		}

		// Token: 0x0600630C RID: 25356 RVA: 0x0015652C File Offset: 0x0015472C
		private void CheckLinkPropertiesAreEmpty()
		{
			ExTraceGlobals.TenantFullSyncTracer.TraceDebug((long)this.TenantExternalDirectoryId.GetHashCode(), "TenantFullSyncPageToken.CheckLinkPropertiesAreEmpty entering");
			if (this.ObjectsInLinkPage != 0)
			{
				ExTraceGlobals.TenantFullSyncTracer.TraceError((long)this.TenantExternalDirectoryId.GetHashCode(), "this.ObjectsInLinkPage != 0");
				throw new InvalidOperationException("ObjectsInLinkPage");
			}
			if (this.LinkPageStart != 0L)
			{
				ExTraceGlobals.TenantFullSyncTracer.TraceError((long)this.TenantExternalDirectoryId.GetHashCode(), "this.LinkPageStart != 0");
				throw new InvalidOperationException("LinkPageStart");
			}
			if (this.LinkPageEnd != 0L)
			{
				ExTraceGlobals.TenantFullSyncTracer.TraceError((long)this.TenantExternalDirectoryId.GetHashCode(), "this.LinkPageEnd != 0");
				throw new InvalidOperationException("LinkPageEnd");
			}
			if (this.LinkRangeStart != 0)
			{
				ExTraceGlobals.TenantFullSyncTracer.TraceError((long)this.TenantExternalDirectoryId.GetHashCode(), "this.LinkRangeStart != 0");
				throw new InvalidOperationException("LinkRangeStart");
			}
		}

		// Token: 0x0600630D RID: 25357 RVA: 0x00156640 File Offset: 0x00154840
		private void SetEnumerateLinksParams(long linkPageStart, long linkPageEnd, int linkRangeStart, int objectsInLinkPage, TenantFullSyncState expectedState)
		{
			ExTraceGlobals.TenantFullSyncTracer.TraceDebug((long)this.TenantExternalDirectoryId.GetHashCode(), "TenantFullSyncPageToken.SetEnumerateLinksParams entering");
			ExTraceGlobals.TenantFullSyncTracer.TraceDebug<int>((long)this.TenantExternalDirectoryId.GetHashCode(), "TenantFullSyncPageToken.SetEnumerateLinksParams objectsInLinkPage = {0}", objectsInLinkPage);
			if (objectsInLinkPage == 0)
			{
				ExTraceGlobals.TenantFullSyncTracer.TraceError((long)this.TenantExternalDirectoryId.GetHashCode(), "objectsInLinkPage != 0");
				throw new ArgumentOutOfRangeException("objectsInLinkPage");
			}
			ExTraceGlobals.TenantFullSyncTracer.TraceDebug<string, string>((long)this.TenantExternalDirectoryId.GetHashCode(), "TenantFullSyncPageToken.SetEnumerateLinksParams this.State = {0}, expectedState = {1}", this.State.ToString(), expectedState.ToString());
			if (this.State != expectedState)
			{
				ExTraceGlobals.TenantFullSyncTracer.TraceError((long)this.TenantExternalDirectoryId.GetHashCode(), "this.State != expectedState");
				throw new InvalidOperationException("State");
			}
			ExTraceGlobals.TenantFullSyncTracer.TraceDebug<long, long>((long)this.TenantExternalDirectoryId.GetHashCode(), "TenantFullSyncPageToken.SetEnumerateLinksParams linkPageStart = {0}, linkPageEnd = {1}", linkPageStart, linkPageEnd);
			if (linkPageStart > linkPageEnd)
			{
				ExTraceGlobals.TenantFullSyncTracer.TraceError((long)this.TenantExternalDirectoryId.GetHashCode(), "linkPageStart > linkPageEnd");
				throw new ArgumentOutOfRangeException("linkPageStart");
			}
			if ((long)objectsInLinkPage > linkPageEnd - linkPageStart + 1L)
			{
				ExTraceGlobals.TenantFullSyncTracer.TraceError((long)this.TenantExternalDirectoryId.GetHashCode(), "objectsInLinkPage > (linkPageEnd - linkPageStart + 1)");
				throw new ArgumentOutOfRangeException("linkPageEnd");
			}
			this.LinkPageStart = linkPageStart;
			this.LinkPageEnd = linkPageEnd;
			this.LinkRangeStart = linkRangeStart;
			this.ObjectsInLinkPage = objectsInLinkPage;
		}

		// Token: 0x0600630E RID: 25358 RVA: 0x001567F8 File Offset: 0x001549F8
		internal void FinishFullSync()
		{
			ExTraceGlobals.TenantFullSyncTracer.TraceDebug((long)this.TenantExternalDirectoryId.GetHashCode(), "TenantFullSyncPageToken.FinishFullSync entering");
			ExTraceGlobals.TenantFullSyncTracer.TraceDebug<string>((long)this.TenantExternalDirectoryId.GetHashCode(), "TenantFullSyncPageToken.FinishFullSync this.State = {0}", this.State.ToString());
			if (this.TenantScopedBackSyncCookie == null && this.State != TenantFullSyncState.EnumerateDeletedObjects)
			{
				ExTraceGlobals.TenantFullSyncTracer.TraceError<TenantFullSyncState>((long)this.TenantExternalDirectoryId.GetHashCode(), "this.State != TenantFullSyncState.EnumerateDeletedObjects. State: {0}", this.State);
				throw new InvalidOperationException("State");
			}
			if (this.TenantScopedBackSyncCookie != null && this.State != TenantFullSyncState.EnumerateLiveObjects && this.State != TenantFullSyncState.EnumerateDeletedObjects)
			{
				ExTraceGlobals.TenantFullSyncTracer.TraceError<TenantFullSyncState>((long)this.TenantExternalDirectoryId.GetHashCode(), "this.State != TenantFullSyncState.EnumerateLiveObjects and EnumerateDeletedObjects. State: {0}", this.State);
				throw new InvalidOperationException("State");
			}
			this.CheckLinkPropertiesAreEmpty();
			if (this.TenantScopedBackSyncCookie == null)
			{
				if (this.PendingWatermarks == null)
				{
					ExTraceGlobals.TenantFullSyncTracer.TraceError((long)this.TenantExternalDirectoryId.GetHashCode(), "this.PendingWatermarks == null");
					throw new InvalidOperationException("PendingWatermarks");
				}
				if (this.PendingWatermarksInvocationId == Guid.Empty)
				{
					ExTraceGlobals.TenantFullSyncTracer.TraceError((long)this.TenantExternalDirectoryId.GetHashCode(), "this.PendingWatermarksInvocationId == Guid.Empty");
					throw new InvalidOperationException("PendingWatermarksInvocationId");
				}
				this.WatermarksInvocationId = this.PendingWatermarksInvocationId;
				this.PendingWatermarksInvocationId = Guid.Empty;
				this.Watermarks = this.PendingWatermarks;
				this.PendingWatermarks = null;
			}
			else
			{
				if (this.TenantScopedBackSyncCookie.MoreDirSyncData)
				{
					ExTraceGlobals.TenantFullSyncTracer.TraceError((long)this.TenantExternalDirectoryId.GetHashCode(), "this.TenantScopedBackSyncCookie.MoreDirSyncData == true");
					throw new InvalidOperationException("TenantScopedBackSyncCookie.MoreDirSyncData");
				}
				ADDirSyncCookie addirSyncCookie = ADDirSyncCookie.Parse(this.TenantScopedBackSyncCookie.DirSyncCookie);
				if (addirSyncCookie.Cursors == null || addirSyncCookie.Cursors.Count == 0)
				{
					ExTraceGlobals.TenantFullSyncTracer.TraceError((long)this.TenantExternalDirectoryId.GetHashCode(), "latestDirSyncCookie.Cursors is null or empty");
					throw new InvalidOperationException("latestDirSyncCookie.Cursors");
				}
				this.WatermarksInvocationId = this.TenantScopedBackSyncCookie.InvocationId;
				this.Watermarks = new WatermarkMap();
				foreach (ReplicationCursor replicationCursor in addirSyncCookie.Cursors)
				{
					this.Watermarks[replicationCursor.SourceInvocationId] = replicationCursor.UpToDatenessUsn;
				}
			}
			this.State = TenantFullSyncState.Complete;
			ExTraceGlobals.TenantFullSyncTracer.TraceDebug((long)this.TenantExternalDirectoryId.GetHashCode(), "Tenant full sync for {0} on {1}({2}) is complete. Watermarks stored: {3}", new object[]
			{
				this.TenantExternalDirectoryId,
				this.InvocationId,
				this.InvocationId,
				this.Watermarks.SerializeToString()
			});
		}

		// Token: 0x0600630F RID: 25359 RVA: 0x00156B24 File Offset: 0x00154D24
		internal void StartNewSyncSequence()
		{
			ExTraceGlobals.TenantFullSyncTracer.TraceDebug<Guid, DateTime>((long)this.TenantExternalDirectoryId.GetHashCode(), "TenantFullSyncPageToken Starting a new sequence. Old sequence info: this.SequenceId = {0} this.SequenceStartTimestamp = {1} ", this.SequenceId, this.SequenceStartTimestamp);
			this.SequenceId = Guid.NewGuid();
			this.SequenceStartTimestamp = DateTime.UtcNow;
			ExTraceGlobals.TenantFullSyncTracer.TraceDebug<Guid, DateTime>((long)this.TenantExternalDirectoryId.GetHashCode(), "TenantFullSyncPageToken Starting a new sequence. New sequence info: this.SequenceId = {0} this.SequenceStartTimestamp = {1} ", this.SequenceId, this.SequenceStartTimestamp);
		}

		// Token: 0x06006310 RID: 25360 RVA: 0x00156BA8 File Offset: 0x00154DA8
		protected void StartMerge()
		{
			ExTraceGlobals.TenantFullSyncTracer.TraceDebug((long)this.TenantExternalDirectoryId.GetHashCode(), "TenantFullSyncPageToken.StartMerge entering");
			ExTraceGlobals.TenantFullSyncTracer.TraceDebug<string>((long)this.TenantExternalDirectoryId.GetHashCode(), "TenantFullSyncPageToken.StartMerge this.State = {0}", this.State.ToString());
			if (this.State != TenantFullSyncState.Complete)
			{
				ExTraceGlobals.TenantFullSyncTracer.TraceError((long)this.TenantExternalDirectoryId.GetHashCode(), "this.State != TenantFullSyncState.Complete");
				throw new InvalidOperationException("State");
			}
			if (this.Watermarks == null)
			{
				ExTraceGlobals.TenantFullSyncTracer.TraceError((long)this.TenantExternalDirectoryId.GetHashCode(), "this.Watermarks == null");
				throw new InvalidOperationException("Watermarks");
			}
			this.CheckLinkPropertiesAreEmpty();
			this.State = TenantFullSyncState.EnumerateLiveObjects;
			this.StartNewSyncSequence();
			ExTraceGlobals.TenantFullSyncTracer.TraceDebug<Guid, Guid, long>((long)this.TenantExternalDirectoryId.GetHashCode(), "[Merge] Starting enumeration of live objects for {0} on {1} from USN {2}", this.TenantExternalDirectoryId, this.InvocationId, this.ObjectUpdateSequenceNumber);
		}

		// Token: 0x04004202 RID: 16898
		internal const int CurrentVersion = 3;

		// Token: 0x04004203 RID: 16899
		internal static BackSyncCookieAttribute[] TenantFullSyncPageTokenAttributeSchema_Version_1 = new BackSyncCookieAttribute[]
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
				Name = "TenantExternalDirectoryId",
				DataType = typeof(Guid),
				DefaultValue = Guid.Empty
			},
			new BackSyncCookieAttribute
			{
				Name = "TenantObjectGuid",
				DataType = typeof(Guid),
				DefaultValue = Guid.Empty
			},
			new BackSyncCookieAttribute
			{
				Name = "TenantFullSyncState",
				DataType = typeof(int),
				DefaultValue = 0
			},
			new BackSyncCookieAttribute
			{
				Name = "ObjectUpdateSequenceNumber",
				DataType = typeof(long),
				DefaultValue = Convert.ToInt64(0)
			},
			new BackSyncCookieAttribute
			{
				Name = "TombstoneUpdateSequenceNumber",
				DataType = typeof(long),
				DefaultValue = Convert.ToInt64(0)
			},
			new BackSyncCookieAttribute
			{
				Name = "PendingWatermarks",
				DataType = typeof(byte[]),
				DefaultValue = null
			},
			new BackSyncCookieAttribute
			{
				Name = "PendingWatermarksInvocationId",
				DataType = typeof(Guid),
				DefaultValue = Guid.Empty
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
			}
		};

		// Token: 0x04004204 RID: 16900
		internal static BackSyncCookieAttribute[] TenantFullSyncPageTokenAttributeSchema_Version_2 = new BackSyncCookieAttribute[]
		{
			new BackSyncCookieAttribute
			{
				Name = "ErrorObjectsAndFailureCounts",
				DataType = typeof(string[]),
				DefaultValue = null
			}
		};

		// Token: 0x04004205 RID: 16901
		internal static BackSyncCookieAttribute[] TenantFullSyncPageTokenAttributeSchema_Version_3 = new BackSyncCookieAttribute[]
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
			},
			new BackSyncCookieAttribute
			{
				Name = "TenantScopedBackSyncCookie",
				DataType = typeof(byte[]),
				DefaultValue = null
			},
			new BackSyncCookieAttribute
			{
				Name = "UseContainerizedUsnChangedIndex",
				DataType = typeof(bool),
				DefaultValue = false
			},
			new BackSyncCookieAttribute
			{
				Name = "SoftDeletedObjectUpdateSequenceNumber",
				DataType = typeof(long),
				DefaultValue = Convert.ToInt64(0)
			},
			new BackSyncCookieAttribute
			{
				Name = "TenantFullSyncPreviousState",
				DataType = typeof(int),
				DefaultValue = 0
			}
		};

		// Token: 0x04004206 RID: 16902
		internal static BackSyncCookieAttribute[][] TenantFullSyncPageTokenAttributeSchemaByVersions = new BackSyncCookieAttribute[][]
		{
			BackSyncCookieAttribute.BackSyncCookieVersionSchema,
			TenantFullSyncPageToken.TenantFullSyncPageTokenAttributeSchema_Version_1,
			TenantFullSyncPageToken.TenantFullSyncPageTokenAttributeSchema_Version_2,
			TenantFullSyncPageToken.TenantFullSyncPageTokenAttributeSchema_Version_3
		};
	}
}
