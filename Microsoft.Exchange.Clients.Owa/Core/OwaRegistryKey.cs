using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001F4 RID: 500
	public sealed class OwaRegistryKey
	{
		// Token: 0x06001074 RID: 4212 RVA: 0x00065488 File Offset: 0x00063688
		public OwaRegistryKey(string name, Type type, object defaultValue)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("name may not be null or empty string");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (defaultValue == null)
			{
				throw new ArgumentNullException("defaultValue");
			}
			if (type != typeof(int) && type != typeof(bool) && type != typeof(string))
			{
				throw new ArgumentException("type must be int, bool or string only, invalid type is: " + type.ToString());
			}
			if (defaultValue.GetType() != type)
			{
				throw new ArgumentException(string.Format("type of defaultValue {0} does not match type {1}", defaultValue.GetType().ToString(), type.ToString()));
			}
			this.name = name;
			this.type = type;
			this.defaultValue = defaultValue;
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06001075 RID: 4213 RVA: 0x00065560 File Offset: 0x00063760
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06001076 RID: 4214 RVA: 0x00065568 File Offset: 0x00063768
		public Type Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06001077 RID: 4215 RVA: 0x00065570 File Offset: 0x00063770
		public object DefaultValue
		{
			get
			{
				return this.defaultValue;
			}
		}

		// Token: 0x04000B23 RID: 2851
		private string name;

		// Token: 0x04000B24 RID: 2852
		private Type type;

		// Token: 0x04000B25 RID: 2853
		private object defaultValue;
	}
}
