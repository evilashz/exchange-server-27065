using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200004C RID: 76
	[DataContract]
	internal sealed class VersionInformation
	{
		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x00007224 File Offset: 0x00005424
		// (set) Token: 0x060003E6 RID: 998 RVA: 0x0000722C File Offset: 0x0000542C
		[DataMember(IsRequired = true)]
		public int ProductMajor { get; set; }

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x00007235 File Offset: 0x00005435
		// (set) Token: 0x060003E8 RID: 1000 RVA: 0x0000723D File Offset: 0x0000543D
		[DataMember(IsRequired = true)]
		public int ProductMinor { get; set; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x00007246 File Offset: 0x00005446
		// (set) Token: 0x060003EA RID: 1002 RVA: 0x0000724E File Offset: 0x0000544E
		[DataMember(IsRequired = true)]
		public int BuildMajor { get; set; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x00007257 File Offset: 0x00005457
		// (set) Token: 0x060003EC RID: 1004 RVA: 0x0000725F File Offset: 0x0000545F
		[DataMember(IsRequired = true)]
		public int BuildMinor { get; set; }

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x00007268 File Offset: 0x00005468
		// (set) Token: 0x060003EE RID: 1006 RVA: 0x00007298 File Offset: 0x00005498
		[DataMember(IsRequired = true)]
		public byte[] SupportedCapabilities
		{
			get
			{
				byte[] array = new byte[(this.supportedCapabilities.Length + 7) / 8];
				this.supportedCapabilities.CopyTo(array, 0);
				return array;
			}
			set
			{
				this.supportedCapabilities = new BitArray(value);
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x000072A6 File Offset: 0x000054A6
		// (set) Token: 0x060003F0 RID: 1008 RVA: 0x000072AE File Offset: 0x000054AE
		[DataMember]
		public string ComputerName { get; set; }

		// Token: 0x060003F1 RID: 1009 RVA: 0x000072B8 File Offset: 0x000054B8
		static VersionInformation()
		{
			VersionInformation.mrsVersion[0] = true;
			VersionInformation.mrsVersion[1] = true;
			VersionInformation.mrsVersion[2] = true;
			VersionInformation.mrsVersion[3] = true;
			VersionInformation.mrsVersion[4] = true;
			VersionInformation.mrsVersion[5] = true;
			VersionInformation.mrsVersion[6] = true;
			VersionInformation.mrsVersion[7] = true;
			VersionInformation.mrsVersion[8] = true;
			VersionInformation.mrsVersion[9] = true;
			VersionInformation.mrsVersion[10] = true;
			VersionInformation.mrsVersion[11] = true;
			VersionInformation.mrsVersion[12] = true;
			VersionInformation.mrsProxyVersion[0] = true;
			VersionInformation.mrsProxyVersion[1] = true;
			VersionInformation.mrsProxyVersion[2] = true;
			VersionInformation.mrsProxyVersion[3] = true;
			VersionInformation.mrsProxyVersion[4] = true;
			VersionInformation.mrsProxyVersion[5] = true;
			VersionInformation.mrsProxyVersion[6] = true;
			VersionInformation.mrsProxyVersion[7] = true;
			VersionInformation.mrsProxyVersion[8] = true;
			VersionInformation.mrsProxyVersion[9] = true;
			VersionInformation.mrsProxyVersion[10] = true;
			VersionInformation.mrsProxyVersion[11] = true;
			VersionInformation.mrsProxyVersion[12] = true;
			VersionInformation.mrsProxyVersion[13] = true;
			VersionInformation.mrsProxyVersion[14] = true;
			VersionInformation.mrsProxyVersion[15] = true;
			VersionInformation.mrsProxyVersion[16] = true;
			VersionInformation.mrsProxyVersion[17] = true;
			VersionInformation.mrsProxyVersion[18] = true;
			VersionInformation.mrsProxyVersion[24] = true;
			VersionInformation.mrsProxyVersion[25] = true;
			VersionInformation.mrsProxyVersion[27] = true;
			VersionInformation.mrsProxyVersion[28] = false;
			VersionInformation.mrsProxyVersion[30] = true;
			VersionInformation.mrsProxyVersion[31] = true;
			VersionInformation.mrsProxyVersion[32] = true;
			VersionInformation.mrsProxyVersion[33] = true;
			VersionInformation.mrsProxyVersion[34] = true;
			VersionInformation.mrsProxyVersion[35] = true;
			VersionInformation.mrsProxyVersion[36] = true;
			VersionInformation.mrsProxyVersion[37] = true;
			VersionInformation.mrsProxyVersion[38] = true;
			VersionInformation.mrsProxyVersion[39] = true;
			VersionInformation.mrsProxyVersion[40] = true;
			VersionInformation.mrsProxyVersion[41] = true;
			VersionInformation.mrsProxyVersion[42] = true;
			VersionInformation.mrsProxyVersion[43] = true;
			VersionInformation.mrsProxyVersion[44] = true;
			VersionInformation.mrsProxyVersion[45] = true;
			VersionInformation.mrsProxyVersion[46] = true;
			VersionInformation.mrsProxyVersion[47] = true;
			VersionInformation.mrsProxyVersion[48] = true;
			VersionInformation.mrsProxyVersion[49] = true;
			VersionInformation.mrsProxyVersion[50] = true;
			VersionInformation.mrsProxyVersion[51] = true;
			VersionInformation.mrsProxyVersion[52] = true;
			VersionInformation.mrsProxyVersion[53] = true;
			VersionInformation.mrsProxyVersion[54] = true;
			VersionInformation.mrsProxyVersion[55] = false;
			VersionInformation.mrsProxyVersion[56] = true;
			VersionInformation.mrsProxyVersion[57] = true;
			VersionInformation.mrsProxyVersion[58] = true;
			VersionInformation.mrsProxyVersion[59] = true;
			VersionInformation.mrsProxyVersion[60] = true;
			VersionInformation.mrsProxyVersion[82] = true;
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00007654 File Offset: 0x00005854
		public VersionInformation(int maxSupportedCapabilities)
		{
			this.ProductMajor = VersionInformation.assemblyFileVersion.FileMajorPart;
			this.ProductMinor = VersionInformation.assemblyFileVersion.FileMinorPart;
			this.BuildMajor = VersionInformation.assemblyFileVersion.FileBuildPart;
			this.BuildMinor = VersionInformation.assemblyFileVersion.FilePrivatePart;
			this.supportedCapabilities = new BitArray(maxSupportedCapabilities);
			this.ComputerName = CommonUtils.LocalComputerName;
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x000076BE File Offset: 0x000058BE
		public static VersionInformation MRS
		{
			get
			{
				return VersionInformation.mrsVersion;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x000076C5 File Offset: 0x000058C5
		public static VersionInformation MRSProxy
		{
			get
			{
				return VersionInformation.mrsProxyVersion;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x000076CC File Offset: 0x000058CC
		public ServerVersion ServerVersion
		{
			get
			{
				return new ServerVersion(this.ProductMajor, this.ProductMinor, this.BuildMajor, this.BuildMinor);
			}
		}

		// Token: 0x17000185 RID: 389
		public bool this[int index]
		{
			get
			{
				return index >= 0 && index < this.supportedCapabilities.Length && this.supportedCapabilities[index];
			}
			set
			{
				this.supportedCapabilities[index] = value;
			}
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000771C File Offset: 0x0000591C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = this.SupportedCapabilities.Length - 1; i >= 0; i--)
			{
				stringBuilder.AppendFormat("{0:X2}", this.SupportedCapabilities[i]);
			}
			return string.Format("{0}.{1}.{2}.{3} caps:{4}", new object[]
			{
				this.ProductMajor,
				this.ProductMinor,
				this.BuildMajor,
				this.BuildMinor,
				stringBuilder.ToString()
			});
		}

		// Token: 0x040002B7 RID: 695
		private BitArray supportedCapabilities;

		// Token: 0x040002B8 RID: 696
		private static readonly FileVersionInfo assemblyFileVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);

		// Token: 0x040002B9 RID: 697
		private static readonly VersionInformation mrsVersion = new VersionInformation(13);

		// Token: 0x040002BA RID: 698
		private static readonly VersionInformation mrsProxyVersion = new VersionInformation(83);
	}
}
