using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009D1 RID: 2513
	[Serializable]
	internal class Sku
	{
		// Token: 0x06005A0A RID: 23050 RVA: 0x00179560 File Offset: 0x00177760
		static Sku()
		{
			Sku.PKConfigurationFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "ProductKeyConfig.xml");
			ExchangeBuild exchangeBuild = ExchangeObjectVersion.Exchange2012.ExchangeBuild;
			Sku.ValidServerVersion = new ServerVersion((int)exchangeBuild.Major, (int)exchangeBuild.Minor, (int)exchangeBuild.Build, (int)exchangeBuild.BuildRevision);
		}

		// Token: 0x06005A0B RID: 23051 RVA: 0x0017960A File Offset: 0x0017780A
		private Sku(string skuCode, ServerEditionType serverEdition, string name, InformationStoreSkuLimits informationStoreSkuLimits)
		{
			this.skuCode = skuCode;
			this.serverEdition = serverEdition;
			this.name = name;
			this.informationStoreSkuLimits = informationStoreSkuLimits;
		}

		// Token: 0x17001AEE RID: 6894
		// (get) Token: 0x06005A0C RID: 23052 RVA: 0x0017962F File Offset: 0x0017782F
		public ServerEditionType ServerEdition
		{
			get
			{
				return this.serverEdition;
			}
		}

		// Token: 0x17001AEF RID: 6895
		// (get) Token: 0x06005A0D RID: 23053 RVA: 0x00179637 File Offset: 0x00177837
		public InformationStoreSkuLimits InformationStoreSkuLimits
		{
			get
			{
				return this.informationStoreSkuLimits;
			}
		}

		// Token: 0x06005A0E RID: 23054 RVA: 0x00179640 File Offset: 0x00177840
		public static Sku GetSku(string productKey)
		{
			Sku result;
			string text;
			Exception ex;
			Sku.TryGenerateProductID(productKey, out result, out text, out ex);
			return result;
		}

		// Token: 0x06005A0F RID: 23055 RVA: 0x0017965C File Offset: 0x0017785C
		public bool IsValidVersion(int versionNumber)
		{
			ServerVersion serverVersion = new ServerVersion(versionNumber);
			return Sku.ValidServerVersion.Major == serverVersion.Major;
		}

		// Token: 0x06005A10 RID: 23056 RVA: 0x00179684 File Offset: 0x00177884
		public string GenerateProductID(string productKey)
		{
			Sku sku;
			string result;
			Exception ex;
			if (!Sku.TryGenerateProductID(productKey, out sku, out result, out ex))
			{
				throw ex;
			}
			if (string.Compare(sku.skuCode, this.skuCode, StringComparison.OrdinalIgnoreCase) != 0)
			{
				throw new InvalidProductKeyException();
			}
			return result;
		}

		// Token: 0x06005A11 RID: 23057 RVA: 0x001796BC File Offset: 0x001778BC
		private static bool TryGenerateProductID(string productKey, out Sku sku, out string pid, out Exception exception)
		{
			Sku[] array = new Sku[]
			{
				Sku.Ent64,
				Sku.Std64,
				Sku.Hybrid
			};
			sku = null;
			pid = null;
			exception = null;
			TaskLogger.Trace("-->GenerateProductId: {0}", new object[]
			{
				productKey
			});
			if (!File.Exists(Sku.PKConfigurationFile))
			{
				exception = new FileNotFoundException(Strings.CannotFindPKConfigFile(Sku.PKConfigurationFile));
				return false;
			}
			Sku.DIGITALPID2 pPID = new Sku.DIGITALPID2();
			Sku.DIGITALPID3 digitalpid = new Sku.DIGITALPID3();
			digitalpid.dwLength = (uint)Marshal.SizeOf(digitalpid);
			Sku.DIGITALPID4 digitalpid2 = new Sku.DIGITALPID4();
			digitalpid2.dwLength = (uint)Marshal.SizeOf(digitalpid2);
			uint num = Sku.PidGenX(productKey, Sku.PKConfigurationFile, "02064", null, pPID, digitalpid, digitalpid2);
			if (num == 0U)
			{
				string text = new string(digitalpid.szSku);
				char[] trimChars = new char[1];
				string strA = text.Trim(trimChars);
				foreach (Sku sku2 in array)
				{
					if (string.Compare(strA, sku2.skuCode, StringComparison.Ordinal) == 0)
					{
						sku = sku2;
						break;
					}
				}
				if (sku != null)
				{
					string text2 = new string(digitalpid.szPid2);
					char[] trimChars2 = new char[1];
					pid = text2.Trim(trimChars2);
					TaskLogger.Trace("<--GenerateProductId: {0}: {1} => valid PID: {2}", new object[]
					{
						sku.name,
						productKey,
						pid
					});
					return true;
				}
				TaskLogger.Trace("<--GenerateProductId: {0} => valid key, but for another SKU.", new object[]
				{
					productKey
				});
				exception = new InvalidProductKeyException();
				return false;
			}
			else
			{
				if (num == 2315321345U)
				{
					TaskLogger.Trace("<--GenerateProductId: {0} => invalid product key config file format. {1}", new object[]
					{
						productKey,
						num
					});
					exception = new InvalidPKConfigFormatException(Sku.PKConfigurationFile);
					return false;
				}
				TaskLogger.Trace("<--GenerateProductId: {0} => invalid product key. {1}", new object[]
				{
					productKey,
					num
				});
				exception = new InvalidProductKeyException();
				return false;
			}
		}

		// Token: 0x06005A12 RID: 23058
		[DllImport("pidgenX.dll")]
		private static extern uint PidGenX([MarshalAs(UnmanagedType.LPWStr)] string pwszProductKey, [MarshalAs(UnmanagedType.LPWStr)] string pwszConfig, [MarshalAs(UnmanagedType.LPWStr)] string pwszMpc, [MarshalAs(UnmanagedType.LPWStr)] string pwszOemId, [Out] Sku.DIGITALPID2 pPID2, [In] [Out] Sku.DIGITALPID3 pPID3, [In] [Out] Sku.DIGITALPID4 pPID4);

		// Token: 0x04003377 RID: 13175
		private const string ProductCode = "02064";

		// Token: 0x04003378 RID: 13176
		private const string Std64SkuCode = "X18-49499";

		// Token: 0x04003379 RID: 13177
		private const string Ent64SkuCode = "X18-49498";

		// Token: 0x0400337A RID: 13178
		private const string HybridSkuCode = "X19-07521";

		// Token: 0x0400337B RID: 13179
		private static readonly string PKConfigurationFile;

		// Token: 0x0400337C RID: 13180
		private static readonly ServerVersion ValidServerVersion;

		// Token: 0x0400337D RID: 13181
		public static readonly Sku Std64 = new Sku("X18-49499", ServerEditionType.Standard, "Standard", InformationStoreSkuLimits.Standard);

		// Token: 0x0400337E RID: 13182
		public static readonly Sku Ent64 = new Sku("X18-49498", ServerEditionType.Enterprise, "Enterprise", InformationStoreSkuLimits.Enterprise);

		// Token: 0x0400337F RID: 13183
		public static readonly Sku Hybrid = new Sku("X19-07521", ServerEditionType.Coexistence, "Hybrid", InformationStoreSkuLimits.Coexistence);

		// Token: 0x04003380 RID: 13184
		private readonly string skuCode;

		// Token: 0x04003381 RID: 13185
		private readonly ServerEditionType serverEdition;

		// Token: 0x04003382 RID: 13186
		private readonly string name;

		// Token: 0x04003383 RID: 13187
		private readonly InformationStoreSkuLimits informationStoreSkuLimits;

		// Token: 0x020009D2 RID: 2514
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4)]
		private class DIGITALPID2
		{
			// Token: 0x04003384 RID: 13188
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 24)]
			public string szPid2;
		}

		// Token: 0x020009D3 RID: 2515
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		private class DIGITALPID3
		{
			// Token: 0x04003385 RID: 13189
			public uint dwLength;

			// Token: 0x04003386 RID: 13190
			public short wVersionMajor;

			// Token: 0x04003387 RID: 13191
			public short wVersionMinor;

			// Token: 0x04003388 RID: 13192
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
			public char[] szPid2;

			// Token: 0x04003389 RID: 13193
			public uint dwKeyIdx;

			// Token: 0x0400338A RID: 13194
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
			public char[] szSku;

			// Token: 0x0400338B RID: 13195
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
			public byte[] abCdKey;

			// Token: 0x0400338C RID: 13196
			public uint dwCloneStatus;

			// Token: 0x0400338D RID: 13197
			public uint dwTime;

			// Token: 0x0400338E RID: 13198
			public uint dwRandom;

			// Token: 0x0400338F RID: 13199
			public uint dwlt;

			// Token: 0x04003390 RID: 13200
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
			public uint[] adwLicenseData;

			// Token: 0x04003391 RID: 13201
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
			public char[] szOemId;

			// Token: 0x04003392 RID: 13202
			public uint dwBundleId;

			// Token: 0x04003393 RID: 13203
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
			public char[] aszHardwareIdStatic;

			// Token: 0x04003394 RID: 13204
			public uint dwHardwareIdTypeStatic;

			// Token: 0x04003395 RID: 13205
			public uint dwBiosChecksumStatic;

			// Token: 0x04003396 RID: 13206
			public uint dwVolSerStatic;

			// Token: 0x04003397 RID: 13207
			public uint dwTotalRamStatic;

			// Token: 0x04003398 RID: 13208
			public uint dwVideoBiosChecksumStatic;

			// Token: 0x04003399 RID: 13209
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
			public char[] aszHardwareIdDynamic;

			// Token: 0x0400339A RID: 13210
			public uint dwHardwareIdTypeDynamic;

			// Token: 0x0400339B RID: 13211
			public uint dwBiosChecksumDynamic;

			// Token: 0x0400339C RID: 13212
			public uint dwVolSerDynamic;

			// Token: 0x0400339D RID: 13213
			public uint dwTotalRamDynamic;

			// Token: 0x0400339E RID: 13214
			public uint dwVideoBiosChecksumDynamic;

			// Token: 0x0400339F RID: 13215
			public uint dwCrc32;
		}

		// Token: 0x020009D4 RID: 2516
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4)]
		private class DIGITALPID4
		{
			// Token: 0x040033A0 RID: 13216
			public uint dwLength;

			// Token: 0x040033A1 RID: 13217
			public ushort wVersionMajor;

			// Token: 0x040033A2 RID: 13218
			public ushort wVersionMinor;

			// Token: 0x040033A3 RID: 13219
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			public string szPid2Ex;

			// Token: 0x040033A4 RID: 13220
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			public string szSku;

			// Token: 0x040033A5 RID: 13221
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
			public string szOemId;

			// Token: 0x040033A6 RID: 13222
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string szEditionId;

			// Token: 0x040033A7 RID: 13223
			public byte bIsUpgrade;

			// Token: 0x040033A8 RID: 13224
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
			public byte[] abReserved;

			// Token: 0x040033A9 RID: 13225
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
			public byte[] abCdKey;

			// Token: 0x040033AA RID: 13226
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
			public byte[] abCdKeySHA256Hash;

			// Token: 0x040033AB RID: 13227
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
			public byte[] abSHA256Hash;

			// Token: 0x040033AC RID: 13228
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			public string szPartNumber;

			// Token: 0x040033AD RID: 13229
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			public string szProductKeyType;

			// Token: 0x040033AE RID: 13230
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			public string szEulaType;
		}
	}
}
