using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000BB RID: 187
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DxStorePropertyNotFoundException : LocalizedException
	{
		// Token: 0x060006D9 RID: 1753 RVA: 0x0001B1ED File Offset: 0x000193ED
		public DxStorePropertyNotFoundException(string propertyName) : base(Strings.DxStorePropertyNotFoundException(propertyName))
		{
			this.propertyName = propertyName;
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x0001B202 File Offset: 0x00019402
		public DxStorePropertyNotFoundException(string propertyName, Exception innerException) : base(Strings.DxStorePropertyNotFoundException(propertyName), innerException)
		{
			this.propertyName = propertyName;
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x0001B218 File Offset: 0x00019418
		protected DxStorePropertyNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.propertyName = (string)info.GetValue("propertyName", typeof(string));
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x0001B242 File Offset: 0x00019442
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("propertyName", this.propertyName);
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060006DD RID: 1757 RVA: 0x0001B25D File Offset: 0x0001945D
		public string PropertyName
		{
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x0400070D RID: 1805
		private readonly string propertyName;
	}
}
