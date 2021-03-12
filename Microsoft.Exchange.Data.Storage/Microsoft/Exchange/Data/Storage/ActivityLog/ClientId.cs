using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.ActivityLog
{
	// Token: 0x02000343 RID: 835
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ClientId
	{
		// Token: 0x17000C69 RID: 3177
		// (get) Token: 0x06002501 RID: 9473 RVA: 0x000954E8 File Offset: 0x000936E8
		private bool IsValidValue
		{
			get
			{
				int num = this.id & int.MaxValue;
				return num >= ClientId.Min.ToInt() && num <= ClientId.Max.ToInt();
			}
		}

		// Token: 0x17000C6A RID: 3178
		// (get) Token: 0x06002502 RID: 9474 RVA: 0x00095521 File Offset: 0x00093721
		public bool LoggedViaServerSideInstrumentation
		{
			get
			{
				return (this.id & int.MinValue) != 0;
			}
		}

		// Token: 0x06002503 RID: 9475 RVA: 0x00095535 File Offset: 0x00093735
		private ClientId(int id)
		{
			this.id = id;
		}

		// Token: 0x06002504 RID: 9476 RVA: 0x00095544 File Offset: 0x00093744
		public static bool operator ==(ClientId id1, ClientId id2)
		{
			return object.ReferenceEquals(id1, id2) || (id1 != null && id2 != null && id1.Equals(id2, false));
		}

		// Token: 0x06002505 RID: 9477 RVA: 0x00095561 File Offset: 0x00093761
		public static bool operator !=(ClientId id1, ClientId id2)
		{
			return !(id1 == id2);
		}

		// Token: 0x06002506 RID: 9478 RVA: 0x0009556D File Offset: 0x0009376D
		public static bool operator <(ClientId id1, ClientId id2)
		{
			return !object.ReferenceEquals(id1, id2) && (id1 == null || (!(id2 == null) && id1.id < id2.id));
		}

		// Token: 0x06002507 RID: 9479 RVA: 0x0009559E File Offset: 0x0009379E
		public static bool operator >(ClientId id1, ClientId id2)
		{
			return !(id1 == id2) && !(id1 < id2);
		}

		// Token: 0x06002508 RID: 9480 RVA: 0x000955B8 File Offset: 0x000937B8
		internal static ClientId FromInt(int intId)
		{
			ClientId clientId = new ClientId(intId);
			if (!clientId.IsValidValue)
			{
				return null;
			}
			return clientId;
		}

		// Token: 0x06002509 RID: 9481 RVA: 0x000955D8 File Offset: 0x000937D8
		internal static ClientId FromString(string s, bool ignoreCase = false)
		{
			if (string.IsNullOrEmpty(s))
			{
				return null;
			}
			int intId;
			if (!int.TryParse(s, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out intId))
			{
				return null;
			}
			return ClientId.FromInt(intId);
		}

		// Token: 0x0600250A RID: 9482 RVA: 0x0009560C File Offset: 0x0009380C
		public override int GetHashCode()
		{
			return this.id.GetHashCode();
		}

		// Token: 0x0600250B RID: 9483 RVA: 0x00095627 File Offset: 0x00093827
		public override bool Equals(object obj)
		{
			return this.Equals(obj, false);
		}

		// Token: 0x0600250C RID: 9484 RVA: 0x00095631 File Offset: 0x00093831
		public bool Equals(object obj, bool ignoreSsi)
		{
			return obj != null && obj is ClientId && this.Equals((ClientId)obj, ignoreSsi);
		}

		// Token: 0x0600250D RID: 9485 RVA: 0x00095650 File Offset: 0x00093850
		public bool Equals(ClientId obj, bool ignoreSsi = false)
		{
			if (obj == null)
			{
				return false;
			}
			int num = this.id ^ obj.id;
			if (ignoreSsi)
			{
				num &= int.MaxValue;
			}
			return num == 0;
		}

		// Token: 0x0600250E RID: 9486 RVA: 0x0009567F File Offset: 0x0009387F
		public ClientId GetServerSideInstrumentationVariant(bool isLoggedViaServerSideInstrumentation)
		{
			if (isLoggedViaServerSideInstrumentation)
			{
				if (this.LoggedViaServerSideInstrumentation)
				{
					return this;
				}
				return ClientId.FromInt(this.id | int.MinValue);
			}
			else
			{
				if (this.LoggedViaServerSideInstrumentation)
				{
					return ClientId.FromInt(this.id & int.MaxValue);
				}
				return this;
			}
		}

		// Token: 0x0600250F RID: 9487 RVA: 0x000956BC File Offset: 0x000938BC
		public override string ToString()
		{
			return this.id.ToString("X8");
		}

		// Token: 0x06002510 RID: 9488 RVA: 0x000956DC File Offset: 0x000938DC
		internal int ToInt()
		{
			return this.id;
		}

		// Token: 0x0400167D RID: 5757
		private const int SsiBit = -2147483648;

		// Token: 0x0400167E RID: 5758
		public static readonly ClientId Min = new ClientId(0);

		// Token: 0x0400167F RID: 5759
		public static readonly ClientId Web = new ClientId(1);

		// Token: 0x04001680 RID: 5760
		public static readonly ClientId Mobile = new ClientId(2);

		// Token: 0x04001681 RID: 5761
		public static readonly ClientId Tablet = new ClientId(3);

		// Token: 0x04001682 RID: 5762
		public static readonly ClientId Desktop = new ClientId(4);

		// Token: 0x04001683 RID: 5763
		public static readonly ClientId Exchange = new ClientId(5);

		// Token: 0x04001684 RID: 5764
		public static readonly ClientId Outlook = new ClientId(6);

		// Token: 0x04001685 RID: 5765
		public static readonly ClientId MacOutlook = new ClientId(7);

		// Token: 0x04001686 RID: 5766
		public static readonly ClientId POP3 = new ClientId(8);

		// Token: 0x04001687 RID: 5767
		public static readonly ClientId IMAP4 = new ClientId(9);

		// Token: 0x04001688 RID: 5768
		public static readonly ClientId Other = new ClientId(10);

		// Token: 0x04001689 RID: 5769
		public static readonly ClientId Lync = new ClientId(11);

		// Token: 0x0400168A RID: 5770
		public static readonly ClientId Max = ClientId.Lync;

		// Token: 0x0400168B RID: 5771
		private readonly int id;
	}
}
