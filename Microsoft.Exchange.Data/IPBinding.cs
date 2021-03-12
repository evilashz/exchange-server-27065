using System;
using System.Net;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200015E RID: 350
	[Serializable]
	public class IPBinding : IPEndPoint
	{
		// Token: 0x06000B4E RID: 2894 RVA: 0x00023563 File Offset: 0x00021763
		public IPBinding(string expression) : base(IPAddress.Any, 0)
		{
			this.ParseAndValidateIPBinding(expression);
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x00023578 File Offset: 0x00021778
		public IPBinding() : base(IPAddress.None, 0)
		{
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x00023586 File Offset: 0x00021786
		public static IPBinding Parse(string ipRange)
		{
			return new IPBinding(ipRange);
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x0002358E File Offset: 0x0002178E
		public IPBinding(IPAddress address, int port) : base(address, port)
		{
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000B52 RID: 2898 RVA: 0x00023598 File Offset: 0x00021798
		// (set) Token: 0x06000B53 RID: 2899 RVA: 0x000235A0 File Offset: 0x000217A0
		public new int Port
		{
			get
			{
				return base.Port;
			}
			set
			{
				if (value < 0 || value > 65535)
				{
					throw new FormatException(DataStrings.ExceptionEndPointPortOutOfRange(0, 65535, value));
				}
				base.Port = value;
			}
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x000235CC File Offset: 0x000217CC
		private void ParseAndValidateIPBinding(string ipBinding)
		{
			ipBinding = ipBinding.Trim();
			if (ipBinding.Length > 0 && ipBinding[ipBinding.Length - 1] == ':')
			{
				ipBinding = ipBinding.Substring(0, ipBinding.Length - 1);
			}
			int num = ipBinding.LastIndexOf(':');
			if (num == -1)
			{
				throw new FormatException(DataStrings.ExceptionEndPointMissingSeparator(ipBinding));
			}
			string text = ipBinding.Substring(0, num);
			IPAddress address;
			if (string.IsNullOrEmpty(text))
			{
				address = new IPAddress(0L);
			}
			else if (!IPAddress.TryParse(text, out address))
			{
				throw new FormatException(DataStrings.ExceptionEndPointInvalidIPAddress(ipBinding));
			}
			int port;
			if (!int.TryParse(ipBinding.Substring(num + 1), out port))
			{
				throw new FormatException(DataStrings.ExceptionEndPointInvalidPort(ipBinding));
			}
			this.Port = port;
			base.Address = address;
		}
	}
}
