using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x0200021C RID: 540
	[Serializable]
	internal abstract class XsoNestedProperty : XsoProperty, INestedProperty
	{
		// Token: 0x0600149B RID: 5275 RVA: 0x0007785C File Offset: 0x00075A5C
		public XsoNestedProperty(INestedData nestedData) : base(null)
		{
			this.nestedData = nestedData;
		}

		// Token: 0x0600149C RID: 5276 RVA: 0x0007786C File Offset: 0x00075A6C
		public XsoNestedProperty(INestedData nestedData, PropertyType type) : base(null, type)
		{
			this.nestedData = nestedData;
		}

		// Token: 0x0600149D RID: 5277 RVA: 0x0007787D File Offset: 0x00075A7D
		public XsoNestedProperty(INestedData nestedData, PropertyDefinition[] prefetchProps) : base(null, prefetchProps)
		{
			this.nestedData = nestedData;
		}

		// Token: 0x0600149E RID: 5278 RVA: 0x0007788E File Offset: 0x00075A8E
		public XsoNestedProperty(INestedData nestedData, PropertyType type, PropertyDefinition[] prefetchProps) : base(null, type, prefetchProps)
		{
			this.nestedData = nestedData;
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x0600149F RID: 5279 RVA: 0x000778A0 File Offset: 0x00075AA0
		// (set) Token: 0x060014A0 RID: 5280 RVA: 0x000778A8 File Offset: 0x00075AA8
		public virtual INestedData NestedData
		{
			get
			{
				return this.nestedData;
			}
			set
			{
				this.nestedData = value;
			}
		}

		// Token: 0x060014A1 RID: 5281 RVA: 0x000778B1 File Offset: 0x00075AB1
		protected override void InternalSetToDefault(IProperty srcProperty)
		{
			this.nestedData.SubProperties.Clear();
		}

		// Token: 0x060014A2 RID: 5282 RVA: 0x000778C3 File Offset: 0x00075AC3
		public override void Bind(StoreObject item)
		{
			this.Unbind();
			base.Bind(item);
		}

		// Token: 0x060014A3 RID: 5283 RVA: 0x000778D2 File Offset: 0x00075AD2
		public override void Unbind()
		{
			this.nestedData.SubProperties.Clear();
			base.Unbind();
		}

		// Token: 0x04000C6B RID: 3179
		private INestedData nestedData;
	}
}
