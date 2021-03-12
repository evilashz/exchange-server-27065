using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020002DE RID: 734
	[Cmdlet("install", "TransportConfigContainer")]
	public sealed class InstallTransportConfigContainerTask : InstallContainerTaskBase<TransportConfigContainer>
	{
		// Token: 0x0600198D RID: 6541 RVA: 0x00071CA8 File Offset: 0x0006FEA8
		protected override void InternalProcessRecord()
		{
			bool flag = base.DataSession.Read<TransportConfigContainer>(this.DataObject.Id) == null;
			MessageDeliveryGlobalSettings[] array = null;
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromAllTenantsOrRootOrgAutoDetect(base.CurrentOrganizationId), 48, "InternalProcessRecord", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\DirectorySetup\\InstallTransportSettingsContainerTask.cs");
			if (flag)
			{
				base.InternalProcessRecord();
				array = tenantOrTopologyConfigurationSession.Find<MessageDeliveryGlobalSettings>(base.CurrentOrgContainerId, QueryScope.SubTree, new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, MessageDeliveryGlobalSettings.DefaultName), null, 1);
			}
			if (base.Organization == null)
			{
				if (flag)
				{
					if (array != null && array.Length == 1)
					{
						this.ApplyTiGlobalSettingsToE12(array[0], tenantOrTopologyConfigurationSession);
						return;
					}
					this.ApplyDefaultTransportSettings(tenantOrTopologyConfigurationSession);
					return;
				}
				else
				{
					this.ApplyE12SettingsToTiGlobalSettings(tenantOrTopologyConfigurationSession);
				}
			}
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x00071D5C File Offset: 0x0006FF5C
		private void ApplyDefaultTransportSettings(IConfigurationSession session)
		{
			TransportConfigContainer[] array = session.Find<TransportConfigContainer>(null, QueryScope.SubTree, null, null, 1);
			if (array == null || array.Length == 0)
			{
				return;
			}
			array[0].MaxRecipientEnvelopeLimit = 500;
			array[0].MaxReceiveSize = ByteQuantifiedSize.FromKB(10240UL);
			array[0].MaxSendSize = ByteQuantifiedSize.FromKB(10240UL);
			session.Save(array[0]);
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x00071DCC File Offset: 0x0006FFCC
		private void ApplyTiGlobalSettingsToE12(MessageDeliveryGlobalSettings settings, IConfigurationSession session)
		{
			TransportConfigContainer[] array = session.Find<TransportConfigContainer>(null, QueryScope.SubTree, null, null, 1);
			if (array == null || array.Length == 0)
			{
				return;
			}
			array[0].MaxRecipientEnvelopeLimit = settings.MaxRecipientEnvelopeLimit;
			array[0].MaxReceiveSize = settings.MaxReceiveSize;
			array[0].MaxSendSize = settings.MaxSendSize;
			session.Save(array[0]);
		}

		// Token: 0x06001990 RID: 6544 RVA: 0x00071E24 File Offset: 0x00070024
		private void ApplyE12SettingsToTiGlobalSettings(IConfigurationSession session)
		{
			TransportConfigContainer[] array = session.Find<TransportConfigContainer>(null, QueryScope.SubTree, null, null, 1);
			if (array == null || array.Length == 0)
			{
				return;
			}
			MessageDeliveryGlobalSettings[] array2 = session.Find<MessageDeliveryGlobalSettings>(session.GetOrgContainerId(), QueryScope.SubTree, new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, MessageDeliveryGlobalSettings.DefaultName), null, 1);
			if (array2 == null || array2.Length == 0)
			{
				return;
			}
			InstallTransportConfigContainerTask.SizeRestriction sizeRestriction = new InstallTransportConfigContainerTask.SizeRestriction(array[0].MaxRecipientEnvelopeLimit, array[0].MaxSendSize, array[0].MaxReceiveSize);
			InstallTransportConfigContainerTask.SizeRestriction sizeRestriction2 = new InstallTransportConfigContainerTask.SizeRestriction(array2[0].MaxRecipientEnvelopeLimit, array2[0].MaxSendSize, array2[0].MaxReceiveSize);
			InstallTransportConfigContainerTask.SizeRestriction sizeRestriction3 = InstallTransportConfigContainerTask.SizeRestriction.Min(sizeRestriction, sizeRestriction2);
			if (!sizeRestriction3.Equals(sizeRestriction))
			{
				array[0].MaxRecipientEnvelopeLimit = sizeRestriction3.MaxRecipientEnvelopeLimit;
				array[0].MaxReceiveSize = sizeRestriction3.MaxReceiveSize;
				array[0].MaxSendSize = sizeRestriction3.MaxSendSize;
				session.Save(array[0]);
			}
			if (!sizeRestriction3.Equals(sizeRestriction2))
			{
				array2[0].MaxRecipientEnvelopeLimit = sizeRestriction3.MaxRecipientEnvelopeLimit;
				array2[0].MaxReceiveSize = sizeRestriction3.MaxReceiveSize;
				array2[0].MaxSendSize = sizeRestriction3.MaxSendSize;
				session.Save(array2[0]);
			}
		}

		// Token: 0x04000B12 RID: 2834
		private const int DefaultMaxRecipientEnvelopeLimit = 500;

		// Token: 0x04000B13 RID: 2835
		private const int DefaultMaxReceiveSize = 10240;

		// Token: 0x04000B14 RID: 2836
		private const int DefaultMaxSendSize = 10240;

		// Token: 0x020002DF RID: 735
		private struct SizeRestriction : IEquatable<InstallTransportConfigContainerTask.SizeRestriction>
		{
			// Token: 0x06001992 RID: 6546 RVA: 0x00071F3E File Offset: 0x0007013E
			public SizeRestriction(Unlimited<int> maxRecipientEnvelopeLimit, Unlimited<ByteQuantifiedSize> maxSendSize, Unlimited<ByteQuantifiedSize> maxReceiveSize)
			{
				this.maxRecipientEnvelopeLimit = maxRecipientEnvelopeLimit;
				this.maxSendSize = maxSendSize;
				this.maxReceiveSize = maxReceiveSize;
			}

			// Token: 0x17000782 RID: 1922
			// (get) Token: 0x06001993 RID: 6547 RVA: 0x00071F55 File Offset: 0x00070155
			public Unlimited<int> MaxRecipientEnvelopeLimit
			{
				get
				{
					return this.maxRecipientEnvelopeLimit;
				}
			}

			// Token: 0x17000783 RID: 1923
			// (get) Token: 0x06001994 RID: 6548 RVA: 0x00071F5D File Offset: 0x0007015D
			public Unlimited<ByteQuantifiedSize> MaxSendSize
			{
				get
				{
					return this.maxSendSize;
				}
			}

			// Token: 0x17000784 RID: 1924
			// (get) Token: 0x06001995 RID: 6549 RVA: 0x00071F65 File Offset: 0x00070165
			public Unlimited<ByteQuantifiedSize> MaxReceiveSize
			{
				get
				{
					return this.maxReceiveSize;
				}
			}

			// Token: 0x06001996 RID: 6550 RVA: 0x00071F6D File Offset: 0x0007016D
			public static InstallTransportConfigContainerTask.SizeRestriction Min(InstallTransportConfigContainerTask.SizeRestriction left, InstallTransportConfigContainerTask.SizeRestriction right)
			{
				return new InstallTransportConfigContainerTask.SizeRestriction(InstallTransportConfigContainerTask.SizeRestriction.Min<int>(left.MaxRecipientEnvelopeLimit, right.MaxRecipientEnvelopeLimit), InstallTransportConfigContainerTask.SizeRestriction.Min<ByteQuantifiedSize>(left.MaxSendSize, right.MaxSendSize), InstallTransportConfigContainerTask.SizeRestriction.Min<ByteQuantifiedSize>(left.MaxReceiveSize, right.MaxReceiveSize));
			}

			// Token: 0x06001997 RID: 6551 RVA: 0x00071FB0 File Offset: 0x000701B0
			public override bool Equals(object other)
			{
				if (other is InstallTransportConfigContainerTask.SizeRestriction)
				{
					InstallTransportConfigContainerTask.SizeRestriction other2 = (InstallTransportConfigContainerTask.SizeRestriction)other;
					return this.Equals(other2);
				}
				return false;
			}

			// Token: 0x06001998 RID: 6552 RVA: 0x00071FD5 File Offset: 0x000701D5
			public bool Equals(InstallTransportConfigContainerTask.SizeRestriction other)
			{
				return this.MaxRecipientEnvelopeLimit == other.MaxRecipientEnvelopeLimit && this.MaxSendSize == other.MaxSendSize && this.MaxReceiveSize == other.MaxReceiveSize;
			}

			// Token: 0x06001999 RID: 6553 RVA: 0x00072014 File Offset: 0x00070214
			public override int GetHashCode()
			{
				return this.MaxRecipientEnvelopeLimit.GetHashCode() ^ this.MaxSendSize.GetHashCode() ^ this.MaxReceiveSize.GetHashCode();
			}

			// Token: 0x0600199A RID: 6554 RVA: 0x0007205F File Offset: 0x0007025F
			private static Unlimited<T> Min<T>(Unlimited<T> left, Unlimited<T> right) where T : struct, IComparable
			{
				if (left.CompareTo(right) < 0)
				{
					return left;
				}
				return right;
			}

			// Token: 0x04000B15 RID: 2837
			private Unlimited<int> maxRecipientEnvelopeLimit;

			// Token: 0x04000B16 RID: 2838
			private Unlimited<ByteQuantifiedSize> maxSendSize;

			// Token: 0x04000B17 RID: 2839
			private Unlimited<ByteQuantifiedSize> maxReceiveSize;
		}
	}
}
