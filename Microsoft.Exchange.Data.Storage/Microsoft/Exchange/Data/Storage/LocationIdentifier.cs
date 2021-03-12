using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000375 RID: 885
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class LocationIdentifier
	{
		// Token: 0x06002708 RID: 9992 RVA: 0x0009C645 File Offset: 0x0009A845
		public LocationIdentifier(uint id) : this(id, LastChangeAction.None)
		{
		}

		// Token: 0x06002709 RID: 9993 RVA: 0x0009C64F File Offset: 0x0009A84F
		public LocationIdentifier(byte[] byteArray)
		{
			this.ByteArray = byteArray;
		}

		// Token: 0x0600270A RID: 9994 RVA: 0x0009C65E File Offset: 0x0009A85E
		internal LocationIdentifier(uint id, LastChangeAction action)
		{
			this.identifier = id;
			this.action = action;
		}

		// Token: 0x0600270B RID: 9995 RVA: 0x0009C674 File Offset: 0x0009A874
		private LocationIdentifier() : this(0U)
		{
		}

		// Token: 0x17000CF6 RID: 3318
		// (get) Token: 0x0600270C RID: 9996 RVA: 0x0009C67D File Offset: 0x0009A87D
		public static int ByteArraySize
		{
			get
			{
				if (LocationIdentifier.byteArraySize == -1)
				{
					LocationIdentifier.byteArraySize = 8;
				}
				return LocationIdentifier.byteArraySize;
			}
		}

		// Token: 0x17000CF7 RID: 3319
		// (get) Token: 0x0600270D RID: 9997 RVA: 0x0009C692 File Offset: 0x0009A892
		public uint Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x17000CF8 RID: 3320
		// (get) Token: 0x0600270E RID: 9998 RVA: 0x0009C69C File Offset: 0x0009A89C
		// (set) Token: 0x0600270F RID: 9999 RVA: 0x0009C6EC File Offset: 0x0009A8EC
		public byte[] ByteArray
		{
			get
			{
				byte[] array = new byte[LocationIdentifier.ByteArraySize];
				byte[] bytes = BitConverter.GetBytes(this.identifier);
				byte[] bytes2 = BitConverter.GetBytes((int)this.action);
				Array.Copy(bytes, array, bytes.Length);
				Array.Copy(bytes2, 0, array, bytes.Length, bytes2.Length);
				return array;
			}
			set
			{
				this.identifier = BitConverter.ToUInt32(value, 0);
				int num = BitConverter.ToInt32(value, 4);
				this.action = (LastChangeAction)num;
			}
		}

		// Token: 0x17000CF9 RID: 3321
		// (get) Token: 0x06002710 RID: 10000 RVA: 0x0009C715 File Offset: 0x0009A915
		internal LastChangeAction Action
		{
			get
			{
				return this.action;
			}
		}

		// Token: 0x06002711 RID: 10001 RVA: 0x0009C720 File Offset: 0x0009A920
		public static LocationIdentifier Parse(string str)
		{
			if (string.IsNullOrEmpty(str))
			{
				throw new ArgumentNullException("str", "Value cannot be null.");
			}
			string[] array = str.Split(new char[]
			{
				':'
			});
			if (array.Length != 2)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "'{0}' is not a valid Location Identifier representation.", new object[]
				{
					str
				}), "str");
			}
			string text = array[0];
			uint num;
			try
			{
				num = uint.Parse(text, CultureInfo.InvariantCulture);
			}
			catch (ArgumentException innerException)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "'{0}' is not a valid Location Identifier.", new object[]
				{
					text
				}), "str", innerException);
			}
			string text2 = array[1];
			LastChangeAction lastChangeAction;
			try
			{
				lastChangeAction = (LastChangeAction)Enum.Parse(typeof(LastChangeAction), text2);
			}
			catch (ArgumentException innerException2)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "'{0}' is not a valid Change Action.", new object[]
				{
					text2
				}), "str", innerException2);
			}
			LocationIdentifier result;
			try
			{
				result = new LocationIdentifier(num, lastChangeAction);
			}
			catch (FormatException innerException3)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "'{0}' is not a valid Location Identifier identifier.", new object[]
				{
					num
				}), "str", innerException3);
			}
			catch (OverflowException innerException4)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "'{0}' is not a valid Location Identifier identifier.", new object[]
				{
					num
				}), "str", innerException4);
			}
			return result;
		}

		// Token: 0x06002712 RID: 10002 RVA: 0x0009C8BC File Offset: 0x0009AABC
		public override int GetHashCode()
		{
			return this.identifier.GetHashCode();
		}

		// Token: 0x06002713 RID: 10003 RVA: 0x0009C8C9 File Offset: 0x0009AAC9
		public override bool Equals(object obj)
		{
			return obj != null && obj is LocationIdentifier && obj.GetHashCode() == this.GetHashCode();
		}

		// Token: 0x06002714 RID: 10004 RVA: 0x0009C8E8 File Offset: 0x0009AAE8
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}:{1}", new object[]
			{
				this.identifier,
				this.action
			});
		}

		// Token: 0x0400172E RID: 5934
		private const int NotCalculated = -1;

		// Token: 0x0400172F RID: 5935
		private static int byteArraySize = -1;

		// Token: 0x04001730 RID: 5936
		private uint identifier;

		// Token: 0x04001731 RID: 5937
		private LastChangeAction action;
	}
}
