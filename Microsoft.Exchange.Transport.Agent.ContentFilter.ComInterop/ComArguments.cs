using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.Exchange.Data.Transport.Interop
{
	// Token: 0x02000002 RID: 2
	[ComVisible(false)]
	internal sealed class ComArguments
	{
		// Token: 0x17000001 RID: 1
		public byte[] this[int id]
		{
			get
			{
				return (byte[])this.props[id];
			}
			set
			{
				if (this.props.Count >= 64 && this.props[id] == null && value != null)
				{
					throw new ArgumentOutOfRangeException(string.Concat(new object[]
					{
						"Reaching the capacity of the property bag (",
						64,
						" properties) while adding property ",
						id,
						"."
					}));
				}
				if (value == null)
				{
					this.props.Remove(id);
					return;
				}
				if (value.Length > 65536)
				{
					string message = string.Concat(new object[]
					{
						"Error trying to set property ",
						id,
						" with size ",
						value.Length,
						". (Maximum: ",
						65536,
						" bytes)"
					});
					throw new ArgumentOutOfRangeException("value", message);
				}
				this.props[id] = value;
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000021E4 File Offset: 0x000003E4
		public int GetInt32(int propertyId)
		{
			return BitConverter.ToInt32(this[propertyId], 0);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000021F4 File Offset: 0x000003F4
		public string GetString(int propertyId)
		{
			byte[] array = this[propertyId];
			if (array == null)
			{
				return string.Empty;
			}
			return Encoding.Unicode.GetString(array);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000221D File Offset: 0x0000041D
		public void SetBool(int propertyId, bool value)
		{
			this[propertyId] = (value ? ComArguments.TrueBytes : ComArguments.FalseBytes);
		}

		// Token: 0x04000001 RID: 1
		private const int MaxPropertySize = 65536;

		// Token: 0x04000002 RID: 2
		private const int MaxNumberOfProperties = 64;

		// Token: 0x04000003 RID: 3
		private static readonly byte[] FalseBytes = BitConverter.GetBytes(false);

		// Token: 0x04000004 RID: 4
		private static readonly byte[] TrueBytes = BitConverter.GetBytes(true);

		// Token: 0x04000005 RID: 5
		private SortedList props = new SortedList();
	}
}
