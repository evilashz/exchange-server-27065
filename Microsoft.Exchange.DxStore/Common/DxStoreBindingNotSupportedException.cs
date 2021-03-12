using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x020000A7 RID: 167
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DxStoreBindingNotSupportedException : DxStoreServerException
	{
		// Token: 0x06000603 RID: 1539 RVA: 0x000147CE File Offset: 0x000129CE
		public DxStoreBindingNotSupportedException(string bindingStr) : base(Strings.DxStoreBindingNotSupportedException(bindingStr))
		{
			this.bindingStr = bindingStr;
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x000147E8 File Offset: 0x000129E8
		public DxStoreBindingNotSupportedException(string bindingStr, Exception innerException) : base(Strings.DxStoreBindingNotSupportedException(bindingStr), innerException)
		{
			this.bindingStr = bindingStr;
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00014803 File Offset: 0x00012A03
		protected DxStoreBindingNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.bindingStr = (string)info.GetValue("bindingStr", typeof(string));
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x0001482D File Offset: 0x00012A2D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("bindingStr", this.bindingStr);
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000607 RID: 1543 RVA: 0x00014848 File Offset: 0x00012A48
		public string BindingStr
		{
			get
			{
				return this.bindingStr;
			}
		}

		// Token: 0x04000295 RID: 661
		private readonly string bindingStr;
	}
}
