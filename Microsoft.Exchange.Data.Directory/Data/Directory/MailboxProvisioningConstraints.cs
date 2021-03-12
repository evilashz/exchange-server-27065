using System;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200006C RID: 108
	[XmlType(TypeName = "MPCs")]
	[Serializable]
	public sealed class MailboxProvisioningConstraints : XMLSerializableBase, IMailboxProvisioningConstraints
	{
		// Token: 0x060004F2 RID: 1266 RVA: 0x0001C4AF File Offset: 0x0001A6AF
		public MailboxProvisioningConstraints()
		{
			this.HardConstraint = new MailboxProvisioningConstraint();
			this.softConstraints = new MailboxProvisioningConstraint[0];
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0001C4CE File Offset: 0x0001A6CE
		public MailboxProvisioningConstraints(MailboxProvisioningConstraint hardConstraint, MailboxProvisioningConstraint[] softConstraints)
		{
			this.HardConstraint = hardConstraint;
			this.softConstraints = softConstraints;
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x0001C4E4 File Offset: 0x0001A6E4
		// (set) Token: 0x060004F5 RID: 1269 RVA: 0x0001C4EC File Offset: 0x0001A6EC
		[XmlElement("Hard")]
		public MailboxProvisioningConstraint HardConstraint { get; set; }

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x0001C4F8 File Offset: 0x0001A6F8
		// (set) Token: 0x060004F7 RID: 1271 RVA: 0x0001C540 File Offset: 0x0001A740
		[XmlElement("Soft")]
		public OrderedMailboxProvisioningConstraint[] SoftConstraints
		{
			get
			{
				OrderedMailboxProvisioningConstraint[] array = new OrderedMailboxProvisioningConstraint[this.softConstraints.Length];
				for (int i = 0; i < this.softConstraints.Length; i++)
				{
					array[i] = new OrderedMailboxProvisioningConstraint(i, this.softConstraints[i].Value);
				}
				return array;
			}
			set
			{
				if (value != null)
				{
					this.softConstraints = new MailboxProvisioningConstraint[value.Length];
					for (int i = 0; i < this.softConstraints.Length; i++)
					{
						this.softConstraints[value[i].Index] = new MailboxProvisioningConstraint(value[i].Value);
					}
				}
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x0001C58D File Offset: 0x0001A78D
		[XmlIgnore]
		IMailboxProvisioningConstraint IMailboxProvisioningConstraints.HardConstraint
		{
			get
			{
				return this.HardConstraint;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x0001C595 File Offset: 0x0001A795
		[XmlIgnore]
		IMailboxProvisioningConstraint[] IMailboxProvisioningConstraints.SoftConstraints
		{
			get
			{
				return this.softConstraints.Cast<IMailboxProvisioningConstraint>().ToArray<IMailboxProvisioningConstraint>();
			}
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0001C5A7 File Offset: 0x0001A7A7
		public bool IsMatch(MailboxProvisioningAttributes attributes)
		{
			return this.HardConstraint == null || this.HardConstraint.IsMatch(attributes);
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0001C5BF File Offset: 0x0001A7BF
		public override bool Equals(object obj)
		{
			return !object.ReferenceEquals(null, obj) && (object.ReferenceEquals(this, obj) || (obj is MailboxProvisioningConstraints && this.Equals((MailboxProvisioningConstraints)obj)));
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0001C5ED File Offset: 0x0001A7ED
		public override int GetHashCode()
		{
			return ((this.HardConstraint != null) ? this.HardConstraint.GetHashCode() : 0) * 397 ^ ((this.SoftConstraints != null) ? this.SoftConstraints.GetHashCode() : 0);
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0001C624 File Offset: 0x0001A824
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("{0};", this.HardConstraint);
			foreach (MailboxProvisioningConstraint arg in this.softConstraints)
			{
				stringBuilder.AppendFormat("{0};", arg);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0001C675 File Offset: 0x0001A875
		private bool Equals(MailboxProvisioningConstraints other)
		{
			return object.Equals(this.HardConstraint, other.HardConstraint) && this.softConstraints.SequenceEqual(other.softConstraints);
		}

		// Token: 0x0400021D RID: 541
		private MailboxProvisioningConstraint[] softConstraints;
	}
}
