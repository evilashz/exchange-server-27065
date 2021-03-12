using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200011B RID: 283
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class TooManyActiveManagerClientRPCsException : LocalizedException
	{
		// Token: 0x06001415 RID: 5141 RVA: 0x0006A37D File Offset: 0x0006857D
		public TooManyActiveManagerClientRPCsException(int maximum) : base(ServerStrings.TooManyActiveManagerClientRPCs(maximum))
		{
			this.maximum = maximum;
		}

		// Token: 0x06001416 RID: 5142 RVA: 0x0006A392 File Offset: 0x00068592
		public TooManyActiveManagerClientRPCsException(int maximum, Exception innerException) : base(ServerStrings.TooManyActiveManagerClientRPCs(maximum), innerException)
		{
			this.maximum = maximum;
		}

		// Token: 0x06001417 RID: 5143 RVA: 0x0006A3A8 File Offset: 0x000685A8
		protected TooManyActiveManagerClientRPCsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.maximum = (int)info.GetValue("maximum", typeof(int));
		}

		// Token: 0x06001418 RID: 5144 RVA: 0x0006A3D2 File Offset: 0x000685D2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("maximum", this.maximum);
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06001419 RID: 5145 RVA: 0x0006A3ED File Offset: 0x000685ED
		public int Maximum
		{
			get
			{
				return this.maximum;
			}
		}

		// Token: 0x040009A9 RID: 2473
		private readonly int maximum;
	}
}
