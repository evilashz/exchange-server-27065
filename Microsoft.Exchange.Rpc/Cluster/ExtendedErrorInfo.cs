using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc.Cluster
{
	// Token: 0x0200012A RID: 298
	[Serializable]
	public sealed class ExtendedErrorInfo
	{
		// Token: 0x060006DB RID: 1755 RVA: 0x0000564C File Offset: 0x00004A4C
		private void BuildToString()
		{
			Exception exception = this.m_Exception;
			if (exception != null)
			{
				this.m_toString = exception.ToString();
			}
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x00005670 File Offset: 0x00004A70
		public ExtendedErrorInfo(Exception exception)
		{
			this.m_Exception = exception;
			this.BuildToString();
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x00005690 File Offset: 0x00004A90
		public sealed override string ToString()
		{
			if (string.IsNullOrEmpty(this.m_toString))
			{
				return base.ToString();
			}
			return this.m_toString;
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x000056B8 File Offset: 0x00004AB8
		[return: MarshalAs(UnmanagedType.U1)]
		public static bool operator ==(ExtendedErrorInfo left, ExtendedErrorInfo right)
		{
			return object.ReferenceEquals(left, right) || (left != null && right != null && left.m_Exception == right.m_Exception);
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x000056E8 File Offset: 0x00004AE8
		[return: MarshalAs(UnmanagedType.U1)]
		public static bool operator !=(ExtendedErrorInfo left, ExtendedErrorInfo right)
		{
			return ((!(left == right)) ? 1 : 0) != 0;
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x00005714 File Offset: 0x00004B14
		public static ExtendedErrorInfo Deserialize(string serializedString)
		{
			return SerializationServices.Deserialize<ExtendedErrorInfo>(Convert.FromBase64String(serializedString));
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x00005700 File Offset: 0x00004B00
		public static ExtendedErrorInfo Deserialize(byte[] serializedBytes)
		{
			return SerializationServices.Deserialize<ExtendedErrorInfo>(serializedBytes);
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x0000572C File Offset: 0x00004B2C
		public byte[] SerializeToBytes()
		{
			return SerializationServices.Serialize(this);
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x00005740 File Offset: 0x00004B40
		public string SerializeToString()
		{
			return Convert.ToBase64String(SerializationServices.Serialize(this));
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x00005758 File Offset: 0x00004B58
		// (set) Token: 0x060006E5 RID: 1765 RVA: 0x0000576C File Offset: 0x00004B6C
		public Exception FailureException
		{
			get
			{
				return this.m_Exception;
			}
			set
			{
				this.m_Exception = value;
			}
		}

		// Token: 0x0400099C RID: 2460
		private Exception m_Exception;

		// Token: 0x0400099D RID: 2461
		private string m_toString;
	}
}
