using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200038B RID: 907
	internal struct ServerObjectHandle : IFormattable, IEquatable<ServerObjectHandle>
	{
		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x060015D4 RID: 5588 RVA: 0x0003874D File Offset: 0x0003694D
		public byte LogonIndex
		{
			get
			{
				return (byte)(this.handleValue >> 24);
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x060015D5 RID: 5589 RVA: 0x00038759 File Offset: 0x00036959
		public uint HandleValue
		{
			get
			{
				return this.handleValue;
			}
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x00038761 File Offset: 0x00036961
		public ServerObjectHandle(uint handleValue)
		{
			this.handleValue = handleValue;
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x0003876A File Offset: 0x0003696A
		public ServerObjectHandle(byte logonIndex, uint counter)
		{
			if (counter > 16777214U)
			{
				throw new ArgumentOutOfRangeException("counter", "Counter too large");
			}
			this.handleValue = ServerObjectHandle.LogonHandle(logonIndex) + counter;
		}

		// Token: 0x060015D8 RID: 5592 RVA: 0x00038794 File Offset: 0x00036994
		public static ServerObjectHandle Parse(Reader reader)
		{
			uint num = reader.ReadUInt32();
			return new ServerObjectHandle(num);
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x000387AE File Offset: 0x000369AE
		internal void Serialize(Writer writer)
		{
			writer.WriteUInt32(this.handleValue);
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x000387BC File Offset: 0x000369BC
		public bool IsLogonHandle(byte logonIndex)
		{
			return this.handleValue == ServerObjectHandle.LogonHandle(logonIndex);
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x000387CC File Offset: 0x000369CC
		public static ServerObjectHandle CreateLogonHandle(byte logonIndex)
		{
			return new ServerObjectHandle(ServerObjectHandle.LogonHandle(logonIndex));
		}

		// Token: 0x060015DC RID: 5596 RVA: 0x000387D9 File Offset: 0x000369D9
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x060015DD RID: 5597 RVA: 0x000387E3 File Offset: 0x000369E3
		private static uint LogonHandle(byte logonIndex)
		{
			return (uint)((uint)logonIndex << 24);
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x000387E9 File Offset: 0x000369E9
		public bool Equals(ServerObjectHandle other)
		{
			return this.handleValue == other.handleValue;
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x000387FC File Offset: 0x000369FC
		public string ToString(string format, IFormatProvider fp)
		{
			if (format != null)
			{
				if (!(format == "B"))
				{
					if (!(format == "G"))
					{
					}
				}
				else
				{
					if (!(this != ServerObjectHandle.None))
					{
						return "?";
					}
					return string.Format(fp, "0x{0:X}", new object[]
					{
						this.handleValue
					});
				}
			}
			return string.Format(fp, "ServerObjectHandle: 0x{0:X}", new object[]
			{
				this.handleValue
			});
		}

		// Token: 0x060015E0 RID: 5600 RVA: 0x00038885 File Offset: 0x00036A85
		public override bool Equals(object obj)
		{
			return obj is ServerObjectHandle && this.Equals((ServerObjectHandle)obj);
		}

		// Token: 0x060015E1 RID: 5601 RVA: 0x000388A0 File Offset: 0x00036AA0
		public override int GetHashCode()
		{
			return this.handleValue.GetHashCode();
		}

		// Token: 0x060015E2 RID: 5602 RVA: 0x000388BB File Offset: 0x00036ABB
		public static bool operator ==(ServerObjectHandle left, ServerObjectHandle right)
		{
			return left.handleValue == right.handleValue;
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x000388CD File Offset: 0x00036ACD
		public static bool operator !=(ServerObjectHandle left, ServerObjectHandle right)
		{
			return left.handleValue != right.handleValue;
		}

		// Token: 0x04000B69 RID: 2921
		private readonly uint handleValue;

		// Token: 0x04000B6A RID: 2922
		public static readonly ServerObjectHandle None = new ServerObjectHandle(uint.MaxValue);

		// Token: 0x04000B6B RID: 2923
		public static readonly ServerObjectHandle ReleasedObject = new ServerObjectHandle(1, 16777214U);
	}
}
