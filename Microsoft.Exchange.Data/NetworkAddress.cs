using System;
using System.ComponentModel;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000255 RID: 597
	[ImmutableObject(true)]
	public abstract class NetworkAddress : ProtocolAddress, IComparable<NetworkAddress>
	{
		// Token: 0x06001436 RID: 5174 RVA: 0x0003FA9C File Offset: 0x0003DC9C
		protected NetworkAddress(NetworkProtocol protocol, string address) : base(protocol, address)
		{
			if (protocol == null)
			{
				throw new ArgumentNullException("protocol");
			}
			if (!NetworkProtocol.IsSupportedProtocol(protocol))
			{
				throw new ArgumentException(DataStrings.ExceptionUnsupportedNetworkProtocol);
			}
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x0003FACC File Offset: 0x0003DCCC
		public static NetworkAddress Parse(string expression)
		{
			NetworkAddress result;
			NetworkAddress.ErrorCode errorCode;
			if (!NetworkAddress.InternalTryParse(expression, out result, out errorCode))
			{
				switch (errorCode)
				{
				case NetworkAddress.ErrorCode.InvalidFormat:
					throw new FormatException(DataStrings.ExceptionInvlidNetworkAddressFormat);
				case NetworkAddress.ErrorCode.UnsupportProtocol:
					throw new ArgumentException(DataStrings.ExceptionUnsupportedNetworkProtocol);
				case NetworkAddress.ErrorCode.InvalidAddress:
					throw new ArgumentOutOfRangeException(DataStrings.ExceptionInvlidProtocolAddressFormat);
				}
			}
			return result;
		}

		// Token: 0x06001438 RID: 5176 RVA: 0x0003FB30 File Offset: 0x0003DD30
		public static bool TryParse(string expression, out NetworkAddress address)
		{
			NetworkAddress.ErrorCode errorCode;
			return NetworkAddress.InternalTryParse(expression, out address, out errorCode);
		}

		// Token: 0x06001439 RID: 5177 RVA: 0x0003FB48 File Offset: 0x0003DD48
		private static bool InternalTryParse(string expression, out NetworkAddress address, out NetworkAddress.ErrorCode error)
		{
			bool result = false;
			address = null;
			error = (NetworkAddress.ErrorCode)0;
			string[] array = expression.Split(new char[]
			{
				':'
			}, 2, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length != 2)
			{
				error = NetworkAddress.ErrorCode.InvalidFormat;
			}
			else
			{
				NetworkProtocol networkProtocol = null;
				try
				{
					networkProtocol = NetworkProtocol.Parse(array[0].Trim());
				}
				catch (ArgumentException)
				{
					error = NetworkAddress.ErrorCode.UnsupportProtocol;
				}
				if (networkProtocol == null)
				{
					error = NetworkAddress.ErrorCode.UnsupportProtocol;
				}
				else
				{
					try
					{
						address = networkProtocol.GetNetworkAddress(array[1].Trim());
						result = true;
					}
					catch (ArgumentOutOfRangeException)
					{
						error = NetworkAddress.ErrorCode.InvalidAddress;
					}
				}
			}
			return result;
		}

		// Token: 0x0600143A RID: 5178 RVA: 0x0003FBD8 File Offset: 0x0003DDD8
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600143B RID: 5179 RVA: 0x0003FBE0 File Offset: 0x0003DDE0
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x0600143C RID: 5180 RVA: 0x0003FBE9 File Offset: 0x0003DDE9
		public static bool operator ==(NetworkAddress a, NetworkAddress b)
		{
			return a == b;
		}

		// Token: 0x0600143D RID: 5181 RVA: 0x0003FBF2 File Offset: 0x0003DDF2
		public static bool operator !=(NetworkAddress a, NetworkAddress b)
		{
			return a != b;
		}

		// Token: 0x0600143E RID: 5182 RVA: 0x0003FBFB File Offset: 0x0003DDFB
		public int CompareTo(NetworkAddress other)
		{
			return base.CompareTo(other);
		}

		// Token: 0x02000256 RID: 598
		private enum ErrorCode
		{
			// Token: 0x04000BE9 RID: 3049
			InvalidFormat = 1,
			// Token: 0x04000BEA RID: 3050
			UnsupportProtocol,
			// Token: 0x04000BEB RID: 3051
			InvalidAddress
		}
	}
}
