using System;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000407 RID: 1031
	[DataContract]
	public class SetIpSafeListing : SetObjectProperties
	{
		// Token: 0x170020BD RID: 8381
		// (get) Token: 0x060034D7 RID: 13527 RVA: 0x000A4E58 File Offset: 0x000A3058
		// (set) Token: 0x060034D8 RID: 13528 RVA: 0x000A4E6F File Offset: 0x000A306F
		[DataMember]
		public string[] InternalServerIPAddresses
		{
			get
			{
				return ((IPAddress[])base["InternalServerIPAddresses"]).ToStringArray();
			}
			set
			{
				base["InternalServerIPAddresses"] = SetIpSafeListing.GetIPAddressArrayFromStringArray(value);
			}
		}

		// Token: 0x170020BE RID: 8382
		// (get) Token: 0x060034D9 RID: 13529 RVA: 0x000A4E82 File Offset: 0x000A3082
		// (set) Token: 0x060034DA RID: 13530 RVA: 0x000A4E99 File Offset: 0x000A3099
		[DataMember]
		public string[] GatewayIPAddresses
		{
			get
			{
				return ((IPAddress[])base["GatewayIPAddresses"]).ToStringArray();
			}
			set
			{
				base["GatewayIPAddresses"] = SetIpSafeListing.GetIPAddressArrayFromStringArray(value);
			}
		}

		// Token: 0x170020BF RID: 8383
		// (get) Token: 0x060034DB RID: 13531 RVA: 0x000A4EAC File Offset: 0x000A30AC
		// (set) Token: 0x060034DC RID: 13532 RVA: 0x000A4EC8 File Offset: 0x000A30C8
		[DataMember]
		public bool IPSkiplistingEnabled
		{
			get
			{
				return (bool)(base["IPSkiplistingEnabled"] ?? false);
			}
			set
			{
				base["IPSkiplistingEnabled"] = value;
			}
		}

		// Token: 0x170020C0 RID: 8384
		// (get) Token: 0x060034DD RID: 13533 RVA: 0x000A4EDB File Offset: 0x000A30DB
		public override string AssociatedCmdlet
		{
			get
			{
				return "Set-PerimeterConfig";
			}
		}

		// Token: 0x170020C1 RID: 8385
		// (get) Token: 0x060034DE RID: 13534 RVA: 0x000A4EE2 File Offset: 0x000A30E2
		public override string RbacScope
		{
			get
			{
				return "@W:Organization";
			}
		}

		// Token: 0x060034DF RID: 13535 RVA: 0x000A4EEC File Offset: 0x000A30EC
		private static IPAddress[] GetIPAddressArrayFromStringArray(string[] value)
		{
			if (value == null)
			{
				return new IPAddress[0];
			}
			IPAddress[] array = new IPAddress[value.Length];
			for (int i = 0; i < value.Length; i++)
			{
				try
				{
					array[i] = IPAddress.Parse(value[i]);
				}
				catch (FormatException)
				{
					throw new FaultException(Strings.InvalidIPAddressFormat(value[i].ToStringWithNull()));
				}
			}
			return array;
		}
	}
}
