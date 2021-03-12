using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Diagnostics.Components.BackSync;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020007B1 RID: 1969
	internal class BackSyncCookieAttribute
	{
		// Token: 0x060061E7 RID: 25063 RVA: 0x0014EEB8 File Offset: 0x0014D0B8
		internal static void CreateBackSyncCookieAttributeDefinitions(byte[] binaryCookie, Type cookieType, out int cookieAttributeCount, out BackSyncCookieAttribute[] backSyncCookieAttributeDefinitions)
		{
			BackSyncCookieAttribute[][] array = null;
			int num = 0;
			if (cookieType.Equals(typeof(BackSyncCookie)))
			{
				array = BackSyncCookie.BackSyncCookieAttributeSchemaByVersions;
				num = 4;
				ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "Cookie schema BackSyncCookie.BackSyncCookieAttributeSchemaByVersions");
			}
			else if (cookieType.Equals(typeof(ObjectFullSyncPageToken)))
			{
				array = ObjectFullSyncPageToken.ObjectFullSyncPageTokenAttributeSchemaByVersions;
				num = 2;
				ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "Cookie schema ObjectFullSyncPageToken.ObjectFullSyncPageTokenAttributeSchemaByVersions");
			}
			else if (cookieType.Equals(typeof(TenantFullSyncPageToken)))
			{
				array = TenantFullSyncPageToken.TenantFullSyncPageTokenAttributeSchemaByVersions;
				num = 3;
				ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "Cookie schema TenantFullSyncPageToken.TenantFullSyncPageTokenAttributeSchemaByVersions");
			}
			else if (cookieType.Equals(typeof(FullSyncObjectCookie)))
			{
				array = FullSyncObjectCookie.FullSyncObjectCookieAttributeSchemaByVersions;
				num = 1;
				ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "Cookie schema FullSyncObjectCookie.FullSyncObjectCookieAttributeSchemaByVersions");
			}
			else if (cookieType.Equals(typeof(MergePageToken)))
			{
				array = MergePageToken.MergePageTokenAttributeSchemaByVersions;
				num = 2;
				ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "Cookie schema MergePageToken.MergePageTokenAttributeSchemaByVersions");
			}
			else if (cookieType.Equals(typeof(TenantRelocationSyncPageToken)))
			{
				array = TenantRelocationSyncPageToken.TenantRelocationSyncPageTokenAttributeSchemaByVersions;
				num = 1;
				ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "Cookie schema TenantRelocationSyncPageToken.TenantRelocationSyncPageTokenAttributeSchemaByVersions");
			}
			else
			{
				ExTraceGlobals.BackSyncTracer.TraceError<string>((long)SyncConfiguration.TraceId, "Invalid cookie type {0}", cookieType.Name);
			}
			ExTraceGlobals.BackSyncTracer.TraceDebug<int>((long)SyncConfiguration.TraceId, "Cookie schema attribute version count = {0}", array.Length);
			ExTraceGlobals.BackSyncTracer.TraceDebug<int>((long)SyncConfiguration.TraceId, "Cookie schema current version (version starts from 0) = {0}", num);
			int num2 = num;
			if (binaryCookie != null)
			{
				ServiceInstanceId arg;
				BackSyncCookieAttribute.ReadCookieVersion(binaryCookie, out num2, out arg);
				ExTraceGlobals.BackSyncTracer.TraceDebug<int>((long)SyncConfiguration.TraceId, "Binary cookie version = {0}", num2);
				ExTraceGlobals.BackSyncTracer.TraceDebug<ServiceInstanceId>((long)SyncConfiguration.TraceId, "Binary cookie service instance id = {0}", arg);
				if (num2 < 0)
				{
					ExTraceGlobals.BackSyncTracer.TraceError((long)SyncConfiguration.TraceId, "Cookie version is less than zero");
					throw new InvalidCookieException();
				}
				num2 = Math.Min(num2, num);
			}
			ExTraceGlobals.BackSyncTracer.TraceDebug<int>((long)SyncConfiguration.TraceId, "Parse cookie using version {0}", num2);
			int num3 = 0;
			int num4 = 0;
			List<BackSyncCookieAttribute> list = new List<BackSyncCookieAttribute>();
			foreach (BackSyncCookieAttribute[] array3 in array)
			{
				ExTraceGlobals.BackSyncTracer.TraceDebug<int>((long)SyncConfiguration.TraceId, "Add attribute from cookie schema version {0}", num4);
				foreach (BackSyncCookieAttribute backSyncCookieAttribute in array3)
				{
					ExTraceGlobals.BackSyncTracer.TraceDebug<string, string>((long)SyncConfiguration.TraceId, "Add attribute {0} ({1})", backSyncCookieAttribute.Name, backSyncCookieAttribute.DataType.Name);
					list.Add(backSyncCookieAttribute);
					if (num4 <= num2)
					{
						num3++;
						ExTraceGlobals.BackSyncTracer.TraceDebug<int>((long)SyncConfiguration.TraceId, "Attribute count = {0}", num3);
					}
				}
				num4++;
			}
			ExTraceGlobals.BackSyncTracer.TraceDebug<int>((long)SyncConfiguration.TraceId, "Return attribute count = {0}", num3);
			cookieAttributeCount = num3;
			backSyncCookieAttributeDefinitions = list.ToArray();
		}

		// Token: 0x060061E8 RID: 25064 RVA: 0x0014F19C File Offset: 0x0014D39C
		private static void ReadCookieVersion(byte[] binaryCookie, out int cookieVersion, out ServiceInstanceId cookieServiceInstanceId)
		{
			int num = 0;
			string serviceInstanceId;
			using (MemoryStream memoryStream = new MemoryStream(binaryCookie))
			{
				using (BinaryReader binaryReader = new BinaryReader(memoryStream))
				{
					num = binaryReader.ReadInt32();
					serviceInstanceId = binaryReader.ReadString();
				}
			}
			cookieVersion = num;
			cookieServiceInstanceId = new ServiceInstanceId(serviceInstanceId);
		}

		// Token: 0x170022ED RID: 8941
		// (get) Token: 0x060061E9 RID: 25065 RVA: 0x0014F208 File Offset: 0x0014D408
		// (set) Token: 0x060061EA RID: 25066 RVA: 0x0014F210 File Offset: 0x0014D410
		internal string Name { get; set; }

		// Token: 0x170022EE RID: 8942
		// (get) Token: 0x060061EB RID: 25067 RVA: 0x0014F219 File Offset: 0x0014D419
		// (set) Token: 0x060061EC RID: 25068 RVA: 0x0014F221 File Offset: 0x0014D421
		internal Type DataType { get; set; }

		// Token: 0x170022EF RID: 8943
		// (get) Token: 0x060061ED RID: 25069 RVA: 0x0014F22A File Offset: 0x0014D42A
		// (set) Token: 0x060061EE RID: 25070 RVA: 0x0014F232 File Offset: 0x0014D432
		internal object DefaultValue { get; set; }

		// Token: 0x060061EF RID: 25071 RVA: 0x0014F23C File Offset: 0x0014D43C
		public override string ToString()
		{
			return string.Format("Name ({0}), Data Type ({1}), Default Value ({2})", this.Name, this.DataType, (this.DefaultValue != null) ? this.DefaultValue.ToString() : "NULL");
		}

		// Token: 0x040041A0 RID: 16800
		internal static BackSyncCookieAttribute[] BackSyncCookieVersionSchema = new BackSyncCookieAttribute[]
		{
			new BackSyncCookieAttribute
			{
				Name = "Version",
				DataType = typeof(int),
				DefaultValue = 0
			},
			new BackSyncCookieAttribute
			{
				Name = "ServiceInstanceId",
				DataType = typeof(string),
				DefaultValue = string.Empty
			}
		};
	}
}
