using System;
using System.Text;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200019D RID: 413
	[Serializable]
	public class ServerCostPair : IEquatable<ServerCostPair>
	{
		// Token: 0x06000D75 RID: 3445 RVA: 0x0002B614 File Offset: 0x00029814
		public ServerCostPair(Guid serverGuid, string serverName, int cost)
		{
			if (cost < 1 || cost > 100)
			{
				throw new ArgumentException(DataStrings.ErrorCostOutOfRange, "Cost");
			}
			if (serverGuid == Guid.Empty && string.IsNullOrEmpty(serverName))
			{
				throw new ArgumentException(DataStrings.ErrorServerGuidAndNameBothEmpty, "ServerName");
			}
			this.serverGuid = serverGuid;
			this.serverName = serverName;
			this.cost = cost;
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000D76 RID: 3446 RVA: 0x0002B684 File Offset: 0x00029884
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000D77 RID: 3447 RVA: 0x0002B68C File Offset: 0x0002988C
		public Guid ServerGuid
		{
			get
			{
				return this.serverGuid;
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06000D78 RID: 3448 RVA: 0x0002B694 File Offset: 0x00029894
		public int Cost
		{
			get
			{
				return this.cost;
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06000D79 RID: 3449 RVA: 0x0002B69C File Offset: 0x0002989C
		public string Name
		{
			get
			{
				return this.ToString();
			}
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x0002B6A4 File Offset: 0x000298A4
		public static ServerCostPair Parse(string input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			string[] array = input.Split(new char[]
			{
				':'
			});
			if (array.Length == 2)
			{
				string text = array[0];
				int num = int.Parse(array[1]);
				return new ServerCostPair(Guid.Empty, text, num);
			}
			throw new FormatException(DataStrings.ErrorInputFormatError);
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x0002B704 File Offset: 0x00029904
		public override string ToString()
		{
			if (!string.IsNullOrEmpty(this.ServerName))
			{
				return this.ServerName + ':' + this.Cost.ToString();
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{");
			stringBuilder.Append(this.ServerGuid.ToString());
			stringBuilder.Append("},");
			stringBuilder.Append(this.cost.ToString());
			return stringBuilder.ToString();
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x0002B790 File Offset: 0x00029990
		public static ServerCostPair ParseFromAD(string adString)
		{
			if (adString == null)
			{
				throw new ArgumentNullException("adString");
			}
			string[] array = adString.Split(new char[]
			{
				','
			});
			if (array.Length == 2 && array[0][0] == '{' && array[0][array[0].Length - 1] == '}')
			{
				string g = array[0].Substring(1, array[0].Length - 2);
				Guid guid = new Guid(g);
				int num = int.Parse(array[1]);
				return new ServerCostPair(guid, string.Empty, num);
			}
			throw new FormatException(DataStrings.ErrorADFormatError);
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x0002B82C File Offset: 0x00029A2C
		public string ToADString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{");
			stringBuilder.Append(this.ServerGuid.ToString());
			stringBuilder.Append("},");
			stringBuilder.Append(this.cost.ToString());
			return stringBuilder.ToString();
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x0002B88C File Offset: 0x00029A8C
		public bool Equals(ServerCostPair other)
		{
			if (other == null)
			{
				return false;
			}
			if (this.cost != other.cost)
			{
				return false;
			}
			if (Guid.Empty != other.serverGuid)
			{
				return this.ServerGuid == other.ServerGuid;
			}
			return string.Equals(this.serverName, other.serverName, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x0002B8E4 File Offset: 0x00029AE4
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ServerCostPair);
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x0002B8F4 File Offset: 0x00029AF4
		public override int GetHashCode()
		{
			if (this.serverGuid != Guid.Empty)
			{
				return this.ServerGuid.GetHashCode();
			}
			return this.ServerName.GetHashCode();
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x0002B933 File Offset: 0x00029B33
		public static bool operator ==(ServerCostPair left, ServerCostPair right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x0002B93C File Offset: 0x00029B3C
		public static bool operator !=(ServerCostPair left, ServerCostPair right)
		{
			return !(left == right);
		}

		// Token: 0x04000845 RID: 2117
		public const int MinCostValue = 1;

		// Token: 0x04000846 RID: 2118
		public const int MaxCostValue = 100;

		// Token: 0x04000847 RID: 2119
		private string serverName;

		// Token: 0x04000848 RID: 2120
		private Guid serverGuid;

		// Token: 0x04000849 RID: 2121
		private int cost;
	}
}
