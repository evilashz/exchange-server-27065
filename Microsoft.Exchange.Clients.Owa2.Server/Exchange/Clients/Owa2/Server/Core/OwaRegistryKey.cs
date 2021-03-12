using System;
using System.Globalization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001F9 RID: 505
	public sealed class OwaRegistryKey
	{
		// Token: 0x060011C8 RID: 4552 RVA: 0x00044958 File Offset: 0x00042B58
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
			if (type != typeof(int) && type != typeof(uint) && type != typeof(bool) && type != typeof(string))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "type must be int, uint, bool or string only, invalid type is: {0}", new object[]
				{
					type
				}));
			}
			if (defaultValue.GetType() != type)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "type of defaultValue {0} does not match type {1}", new object[]
				{
					defaultValue.GetType().ToString(),
					type.ToString()
				}));
			}
			this.Name = name;
			this.KeyType = type;
			this.DefaultValue = defaultValue;
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x060011C9 RID: 4553 RVA: 0x00044A60 File Offset: 0x00042C60
		// (set) Token: 0x060011CA RID: 4554 RVA: 0x00044A68 File Offset: 0x00042C68
		public string Name { get; private set; }

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x060011CB RID: 4555 RVA: 0x00044A71 File Offset: 0x00042C71
		// (set) Token: 0x060011CC RID: 4556 RVA: 0x00044A79 File Offset: 0x00042C79
		public Type KeyType { get; private set; }

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x060011CD RID: 4557 RVA: 0x00044A82 File Offset: 0x00042C82
		// (set) Token: 0x060011CE RID: 4558 RVA: 0x00044A8A File Offset: 0x00042C8A
		public object DefaultValue { get; private set; }
	}
}
