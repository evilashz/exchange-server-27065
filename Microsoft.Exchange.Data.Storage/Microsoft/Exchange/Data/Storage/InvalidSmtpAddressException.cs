using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000110 RID: 272
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidSmtpAddressException : MigrationPermanentException
	{
		// Token: 0x060013DF RID: 5087 RVA: 0x00069E46 File Offset: 0x00068046
		public InvalidSmtpAddressException(string address) : base(ServerStrings.InvalidSmtpAddress(address))
		{
			this.address = address;
		}

		// Token: 0x060013E0 RID: 5088 RVA: 0x00069E5B File Offset: 0x0006805B
		public InvalidSmtpAddressException(string address, Exception innerException) : base(ServerStrings.InvalidSmtpAddress(address), innerException)
		{
			this.address = address;
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x00069E71 File Offset: 0x00068071
		protected InvalidSmtpAddressException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.address = (string)info.GetValue("address", typeof(string));
		}

		// Token: 0x060013E2 RID: 5090 RVA: 0x00069E9B File Offset: 0x0006809B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("address", this.address);
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x060013E3 RID: 5091 RVA: 0x00069EB6 File Offset: 0x000680B6
		public string Address
		{
			get
			{
				return this.address;
			}
		}

		// Token: 0x0400099E RID: 2462
		private readonly string address;
	}
}
