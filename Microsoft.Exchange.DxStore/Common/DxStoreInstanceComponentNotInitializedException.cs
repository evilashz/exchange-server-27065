using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x020000A2 RID: 162
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DxStoreInstanceComponentNotInitializedException : DxStoreInstanceServerException
	{
		// Token: 0x060005EB RID: 1515 RVA: 0x0001458D File Offset: 0x0001278D
		public DxStoreInstanceComponentNotInitializedException(string component) : base(Strings.DxStoreInstanceComponentNotInitialized(component))
		{
			this.component = component;
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x000145A7 File Offset: 0x000127A7
		public DxStoreInstanceComponentNotInitializedException(string component, Exception innerException) : base(Strings.DxStoreInstanceComponentNotInitialized(component), innerException)
		{
			this.component = component;
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x000145C2 File Offset: 0x000127C2
		protected DxStoreInstanceComponentNotInitializedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.component = (string)info.GetValue("component", typeof(string));
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x000145EC File Offset: 0x000127EC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("component", this.component);
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x00014607 File Offset: 0x00012807
		public string Component
		{
			get
			{
				return this.component;
			}
		}

		// Token: 0x04000291 RID: 657
		private readonly string component;
	}
}
