using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000AF7 RID: 2807
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class PropertyBagContext
	{
		// Token: 0x060065E4 RID: 26084 RVA: 0x001B0E21 File Offset: 0x001AF021
		public PropertyBagContext()
		{
		}

		// Token: 0x060065E5 RID: 26085 RVA: 0x001B0E2C File Offset: 0x001AF02C
		public PropertyBagContext(PropertyBagContext other)
		{
			if (other == null)
			{
				return;
			}
			this.session = other.session;
			this.coreState = other.coreState;
			this.coreObject = other.coreObject;
			this.storeObject = other.storeObject;
			this.isValidationDisabled = other.isValidationDisabled;
		}

		// Token: 0x060065E6 RID: 26086 RVA: 0x001B0E80 File Offset: 0x001AF080
		public void Copy(PropertyBagContext other)
		{
			if (other == null)
			{
				return;
			}
			this.session = other.session;
			this.coreState = other.coreState;
			this.coreObject = other.coreObject;
			this.storeObject = other.storeObject;
			this.isValidationDisabled = other.isValidationDisabled;
		}

		// Token: 0x17001C1C RID: 7196
		// (get) Token: 0x060065E7 RID: 26087 RVA: 0x001B0ECD File Offset: 0x001AF0CD
		// (set) Token: 0x060065E8 RID: 26088 RVA: 0x001B0ED5 File Offset: 0x001AF0D5
		public StoreSession Session
		{
			get
			{
				return this.session;
			}
			set
			{
				this.session = value;
			}
		}

		// Token: 0x17001C1D RID: 7197
		// (get) Token: 0x060065E9 RID: 26089 RVA: 0x001B0EDE File Offset: 0x001AF0DE
		// (set) Token: 0x060065EA RID: 26090 RVA: 0x001B0EE6 File Offset: 0x001AF0E6
		public ICoreObject CoreObject
		{
			get
			{
				return this.coreObject;
			}
			set
			{
				this.coreState = null;
				this.coreObject = value;
			}
		}

		// Token: 0x17001C1E RID: 7198
		// (get) Token: 0x060065EB RID: 26091 RVA: 0x001B0EF6 File Offset: 0x001AF0F6
		// (set) Token: 0x060065EC RID: 26092 RVA: 0x001B0F0D File Offset: 0x001AF10D
		public ICoreState CoreState
		{
			get
			{
				if (this.coreState != null)
				{
					return this.coreState;
				}
				return this.CoreObject;
			}
			set
			{
				this.coreState = value;
			}
		}

		// Token: 0x17001C1F RID: 7199
		// (get) Token: 0x060065ED RID: 26093 RVA: 0x001B0F16 File Offset: 0x001AF116
		// (set) Token: 0x060065EE RID: 26094 RVA: 0x001B0F1E File Offset: 0x001AF11E
		public StoreObject StoreObject
		{
			get
			{
				return this.storeObject;
			}
			set
			{
				this.storeObject = value;
			}
		}

		// Token: 0x17001C20 RID: 7200
		// (get) Token: 0x060065EF RID: 26095 RVA: 0x001B0F27 File Offset: 0x001AF127
		// (set) Token: 0x060065F0 RID: 26096 RVA: 0x001B0F2F File Offset: 0x001AF12F
		internal bool IsValidationDisabled
		{
			get
			{
				return this.isValidationDisabled;
			}
			set
			{
				this.isValidationDisabled = value;
			}
		}

		// Token: 0x17001C21 RID: 7201
		// (get) Token: 0x060065F1 RID: 26097 RVA: 0x001B0F38 File Offset: 0x001AF138
		// (set) Token: 0x060065F2 RID: 26098 RVA: 0x001B0F40 File Offset: 0x001AF140
		internal AutomaticContactLinkingAction AutomaticContactLinkingAction { get; set; }

		// Token: 0x040039F1 RID: 14833
		private StoreSession session;

		// Token: 0x040039F2 RID: 14834
		private ICoreObject coreObject;

		// Token: 0x040039F3 RID: 14835
		private ICoreState coreState;

		// Token: 0x040039F4 RID: 14836
		private StoreObject storeObject;

		// Token: 0x040039F5 RID: 14837
		private bool isValidationDisabled;
	}
}
