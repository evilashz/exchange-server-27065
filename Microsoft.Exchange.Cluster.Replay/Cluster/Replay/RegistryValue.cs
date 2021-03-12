using System;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Win32;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020000BF RID: 191
	internal class RegistryValue
	{
		// Token: 0x060007CE RID: 1998 RVA: 0x000263F1 File Offset: 0x000245F1
		public RegistryValue()
		{
			this.name = null;
			this.value = null;
			this.kind = RegistryValueKind.Unknown;
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x0002640E File Offset: 0x0002460E
		public RegistryValue(string name, object value, RegistryValueKind kind)
		{
			this.name = name;
			this.value = value;
			this.kind = kind;
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060007D0 RID: 2000 RVA: 0x0002642B File Offset: 0x0002462B
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060007D1 RID: 2001 RVA: 0x00026433 File Offset: 0x00024633
		public RegistryValueKind Kind
		{
			get
			{
				return this.kind;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060007D2 RID: 2002 RVA: 0x0002643B File Offset: 0x0002463B
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x00026444 File Offset: 0x00024644
		public bool Equals(RegistryValue other)
		{
			int num = 0;
			bool result = true;
			if (other == null || other.Value == null)
			{
				result = false;
			}
			else
			{
				if (this.Kind == other.Kind && string.Compare(this.Name, other.Name, StringComparison.OrdinalIgnoreCase) == 0)
				{
					switch (this.Kind)
					{
					case RegistryValueKind.String:
					case RegistryValueKind.ExpandString:
						return SharedHelper.StringIEquals((string)this.Value, (string)other.Value);
					case RegistryValueKind.Binary:
					{
						byte[] array = (byte[])other.Value;
						byte[] array2 = (byte[])this.Value;
						if (array.Length == array2.Length)
						{
							foreach (byte b in array2)
							{
								if (b != array[num])
								{
									result = false;
									break;
								}
								num++;
							}
							return result;
						}
						return false;
					}
					case RegistryValueKind.DWord:
						return (int)this.Value == (int)other.Value;
					case RegistryValueKind.MultiString:
					{
						string[] array4 = (string[])other.Value;
						string[] array5 = (string[])this.Value;
						if (array4.Length == array5.Length)
						{
							foreach (string str in array5)
							{
								if (!SharedHelper.StringIEquals(str, array4[num]))
								{
									result = false;
									break;
								}
								num++;
							}
							return result;
						}
						return false;
					}
					case RegistryValueKind.QWord:
						return (long)this.Value == (long)other.Value;
					}
					throw new NotImplementedException();
				}
				result = false;
			}
			return result;
		}

		// Token: 0x04000378 RID: 888
		private string name;

		// Token: 0x04000379 RID: 889
		private RegistryValueKind kind;

		// Token: 0x0400037A RID: 890
		private object value;
	}
}
