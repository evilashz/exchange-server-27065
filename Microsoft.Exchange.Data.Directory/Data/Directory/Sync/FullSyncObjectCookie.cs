using System;
using System.IO;
using Microsoft.Exchange.Diagnostics.Components.BackSync;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020007C4 RID: 1988
	internal sealed class FullSyncObjectCookie
	{
		// Token: 0x060062A5 RID: 25253 RVA: 0x00154D0F File Offset: 0x00152F0F
		private FullSyncObjectCookie()
		{
			ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "New FullSyncObjectCookie");
			this.Version = 1;
		}

		// Token: 0x060062A6 RID: 25254 RVA: 0x00154D33 File Offset: 0x00152F33
		internal FullSyncObjectCookie(SyncObjectId objectId, LinkMetadata overlapLink, int nextRangeStart, long usnChanged, ServiceInstanceId serviceInstanceid) : this()
		{
			this.ReadRestartsCount = 0;
			this.SetNextPageData(objectId, overlapLink, nextRangeStart, usnChanged);
			this.ServiceInstanceId = serviceInstanceid;
		}

		// Token: 0x17002310 RID: 8976
		// (get) Token: 0x060062A7 RID: 25255 RVA: 0x00154D55 File Offset: 0x00152F55
		// (set) Token: 0x060062A8 RID: 25256 RVA: 0x00154D5D File Offset: 0x00152F5D
		internal int Version { get; private set; }

		// Token: 0x17002311 RID: 8977
		// (get) Token: 0x060062A9 RID: 25257 RVA: 0x00154D66 File Offset: 0x00152F66
		// (set) Token: 0x060062AA RID: 25258 RVA: 0x00154D6E File Offset: 0x00152F6E
		internal long UsnChanged { get; private set; }

		// Token: 0x17002312 RID: 8978
		// (get) Token: 0x060062AB RID: 25259 RVA: 0x00154D77 File Offset: 0x00152F77
		// (set) Token: 0x060062AC RID: 25260 RVA: 0x00154D7F File Offset: 0x00152F7F
		internal int ReadRestartsCount { get; private set; }

		// Token: 0x17002313 RID: 8979
		// (get) Token: 0x060062AD RID: 25261 RVA: 0x00154D88 File Offset: 0x00152F88
		// (set) Token: 0x060062AE RID: 25262 RVA: 0x00154D90 File Offset: 0x00152F90
		internal bool EnumerateLinks { get; private set; }

		// Token: 0x17002314 RID: 8980
		// (get) Token: 0x060062AF RID: 25263 RVA: 0x00154D99 File Offset: 0x00152F99
		// (set) Token: 0x060062B0 RID: 25264 RVA: 0x00154DA1 File Offset: 0x00152FA1
		internal int LinkRangeStart { get; private set; }

		// Token: 0x17002315 RID: 8981
		// (get) Token: 0x060062B1 RID: 25265 RVA: 0x00154DAA File Offset: 0x00152FAA
		// (set) Token: 0x060062B2 RID: 25266 RVA: 0x00154DB2 File Offset: 0x00152FB2
		internal int LinkVersion { get; private set; }

		// Token: 0x17002316 RID: 8982
		// (get) Token: 0x060062B3 RID: 25267 RVA: 0x00154DBB File Offset: 0x00152FBB
		// (set) Token: 0x060062B4 RID: 25268 RVA: 0x00154DC3 File Offset: 0x00152FC3
		internal string LinkTarget { get; private set; }

		// Token: 0x17002317 RID: 8983
		// (get) Token: 0x060062B5 RID: 25269 RVA: 0x00154DCC File Offset: 0x00152FCC
		// (set) Token: 0x060062B6 RID: 25270 RVA: 0x00154DD4 File Offset: 0x00152FD4
		internal string LinkName { get; private set; }

		// Token: 0x17002318 RID: 8984
		// (get) Token: 0x060062B7 RID: 25271 RVA: 0x00154DDD File Offset: 0x00152FDD
		// (set) Token: 0x060062B8 RID: 25272 RVA: 0x00154DE5 File Offset: 0x00152FE5
		internal SyncObjectId ObjectId { get; private set; }

		// Token: 0x17002319 RID: 8985
		// (get) Token: 0x060062B9 RID: 25273 RVA: 0x00154DEE File Offset: 0x00152FEE
		// (set) Token: 0x060062BA RID: 25274 RVA: 0x00154DF6 File Offset: 0x00152FF6
		internal ServiceInstanceId ServiceInstanceId { get; private set; }

		// Token: 0x060062BB RID: 25275 RVA: 0x00154E00 File Offset: 0x00153000
		public static FullSyncObjectCookie Parse(byte[] bytes)
		{
			ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "FullSyncObjectCookie.Parse entering");
			if (bytes == null)
			{
				ExTraceGlobals.BackSyncTracer.TraceError((long)SyncConfiguration.TraceId, "FullSyncObjectCookie.Parse bytes is NULL");
				throw new ArgumentNullException("bytes");
			}
			Exception ex2;
			try
			{
				using (BackSyncCookieReader backSyncCookieReader = BackSyncCookieReader.Create(bytes, typeof(FullSyncObjectCookie)))
				{
					return new FullSyncObjectCookie
					{
						Version = (int)backSyncCookieReader.GetNextAttributeValue(),
						ServiceInstanceId = new ServiceInstanceId((string)backSyncCookieReader.GetNextAttributeValue()),
						ObjectId = SyncObjectId.Parse((string)backSyncCookieReader.GetNextAttributeValue()),
						ReadRestartsCount = (int)backSyncCookieReader.GetNextAttributeValue(),
						UsnChanged = (long)backSyncCookieReader.GetNextAttributeValue(),
						EnumerateLinks = (bool)backSyncCookieReader.GetNextAttributeValue(),
						LinkRangeStart = (int)backSyncCookieReader.GetNextAttributeValue(),
						LinkVersion = (int)backSyncCookieReader.GetNextAttributeValue(),
						LinkTarget = (string)backSyncCookieReader.GetNextAttributeValue(),
						LinkName = (string)backSyncCookieReader.GetNextAttributeValue()
					};
				}
			}
			catch (ArgumentException ex)
			{
				ExTraceGlobals.BackSyncTracer.TraceError<string>((long)SyncConfiguration.TraceId, "FullSyncObjectCookie.Parse ArgumentException {0}", ex.ToString());
				ex2 = ex;
			}
			catch (IOException ex3)
			{
				ExTraceGlobals.BackSyncTracer.TraceError<string>((long)SyncConfiguration.TraceId, "FullSyncObjectCookie.Parse IOException {0}", ex3.ToString());
				ex2 = ex3;
			}
			catch (FormatException ex4)
			{
				ExTraceGlobals.BackSyncTracer.TraceError<string>((long)SyncConfiguration.TraceId, "FullSyncObjectCookie.Parse FormatException {0}", ex4.ToString());
				ex2 = ex4;
			}
			ExTraceGlobals.BackSyncTracer.TraceError<string>((long)SyncConfiguration.TraceId, "FullSyncObjectCookie.Parse throw InvalidCookieException {0}", ex2.ToString());
			throw new InvalidCookieException(ex2);
		}

		// Token: 0x060062BC RID: 25276 RVA: 0x00154FE0 File Offset: 0x001531E0
		public byte[] ToByteArray()
		{
			ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "FullSyncObjectCookie.ToByteArray entering");
			byte[] result = null;
			using (BackSyncCookieWriter backSyncCookieWriter = BackSyncCookieWriter.Create(typeof(FullSyncObjectCookie)))
			{
				backSyncCookieWriter.WriteNextAttributeValue(this.Version);
				backSyncCookieWriter.WriteNextAttributeValue(this.ServiceInstanceId.InstanceId);
				backSyncCookieWriter.WriteNextAttributeValue(this.ObjectId.ToString());
				backSyncCookieWriter.WriteNextAttributeValue(this.ReadRestartsCount);
				backSyncCookieWriter.WriteNextAttributeValue(this.UsnChanged);
				backSyncCookieWriter.WriteNextAttributeValue(this.EnumerateLinks);
				backSyncCookieWriter.WriteNextAttributeValue(this.LinkRangeStart);
				backSyncCookieWriter.WriteNextAttributeValue(this.LinkVersion);
				backSyncCookieWriter.WriteNextAttributeValue(this.LinkTarget);
				backSyncCookieWriter.WriteNextAttributeValue(this.LinkName);
				result = backSyncCookieWriter.GetBinaryCookie();
			}
			return result;
		}

		// Token: 0x060062BD RID: 25277 RVA: 0x001550D8 File Offset: 0x001532D8
		public bool ContainsOverlapLink(LinkMetadata metadata)
		{
			bool flag = metadata != null && metadata.AttributeName.Equals(this.LinkName, StringComparison.OrdinalIgnoreCase) && metadata.TargetDistinguishedName.Equals(this.LinkTarget, StringComparison.OrdinalIgnoreCase) && metadata.Version == this.LinkVersion;
			ExTraceGlobals.BackSyncTracer.TraceDebug<bool>((long)SyncConfiguration.TraceId, "FullSyncObjectCookie.ContainsOverlapLink return {0}", flag);
			return flag;
		}

		// Token: 0x060062BE RID: 25278 RVA: 0x00155139 File Offset: 0x00153339
		public void RestartObjectReadAfterTargetLinksChange()
		{
			ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "FullSyncObjectCookie.RestartObjectReadAfterTargetLinksChange entering");
			this.RestartObjectRead(true);
		}

		// Token: 0x060062BF RID: 25279 RVA: 0x00155157 File Offset: 0x00153357
		public void RestartObjectReadAfterObjectChange(long usnChanged)
		{
			ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "FullSyncObjectCookie.RestartObjectReadAfterObjectChange entering");
			this.RestartObjectRead(false);
			this.UsnChanged = usnChanged;
		}

		// Token: 0x060062C0 RID: 25280 RVA: 0x0015517C File Offset: 0x0015337C
		public void RestartObjectRead(bool restartOnlyLinkEnumeration)
		{
			ExTraceGlobals.BackSyncTracer.TraceDebug<bool>((long)SyncConfiguration.TraceId, "FullSyncObjectCookie.RestartObjectRead (restartOnlyLinkEnumeration = {0}) entering", restartOnlyLinkEnumeration);
			this.EnumerateLinks = restartOnlyLinkEnumeration;
			this.LinkName = null;
			this.LinkRangeStart = 0;
			this.LinkTarget = null;
			this.LinkVersion = 0;
			this.ReadRestartsCount++;
		}

		// Token: 0x060062C1 RID: 25281 RVA: 0x001551D0 File Offset: 0x001533D0
		public void SetNextPageData(SyncObjectId objectId, LinkMetadata overlapLink, int nextRangeStart, long usnChanged)
		{
			this.ObjectId = objectId;
			ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "FullSyncObjectCookie.SetNextPageData this.ObjectId = {0}", (this.ObjectId != null) ? this.ObjectId.ObjectId : "NULL");
			this.EnumerateLinks = true;
			this.LinkName = overlapLink.AttributeName;
			this.LinkRangeStart = nextRangeStart;
			this.LinkTarget = overlapLink.TargetDistinguishedName;
			this.LinkVersion = overlapLink.Version;
			this.UsnChanged = usnChanged;
		}

		// Token: 0x040041F1 RID: 16881
		internal const int CurrentVersion = 1;

		// Token: 0x040041F2 RID: 16882
		internal static BackSyncCookieAttribute[] FullSyncObjectCookieAttributeSchema_Version_1 = new BackSyncCookieAttribute[]
		{
			new BackSyncCookieAttribute
			{
				Name = "ObjectId",
				DataType = typeof(string),
				DefaultValue = string.Empty
			},
			new BackSyncCookieAttribute
			{
				Name = "ReadRestartsCount",
				DataType = typeof(int),
				DefaultValue = 0
			},
			new BackSyncCookieAttribute
			{
				Name = "UsnChanged",
				DataType = typeof(long),
				DefaultValue = Convert.ToInt64(0)
			},
			new BackSyncCookieAttribute
			{
				Name = "EnumerateLinks",
				DataType = typeof(bool),
				DefaultValue = false
			},
			new BackSyncCookieAttribute
			{
				Name = "LinkRangeStart",
				DataType = typeof(int),
				DefaultValue = 0
			},
			new BackSyncCookieAttribute
			{
				Name = "LinkVersion",
				DataType = typeof(int),
				DefaultValue = 0
			},
			new BackSyncCookieAttribute
			{
				Name = "LinkTarget",
				DataType = typeof(string),
				DefaultValue = string.Empty
			},
			new BackSyncCookieAttribute
			{
				Name = "LinkName",
				DataType = typeof(string),
				DefaultValue = string.Empty
			}
		};

		// Token: 0x040041F3 RID: 16883
		internal static BackSyncCookieAttribute[][] FullSyncObjectCookieAttributeSchemaByVersions = new BackSyncCookieAttribute[][]
		{
			BackSyncCookieAttribute.BackSyncCookieVersionSchema,
			FullSyncObjectCookie.FullSyncObjectCookieAttributeSchema_Version_1
		};
	}
}
