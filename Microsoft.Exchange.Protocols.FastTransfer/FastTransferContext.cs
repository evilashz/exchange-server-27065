using System;
using Microsoft.Exchange.Protocols.MAPI;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.FastTransfer;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PropTags;

namespace Microsoft.Exchange.Protocols.FastTransfer
{
	// Token: 0x02000003 RID: 3
	internal class FastTransferContext : MapiBase, ISession
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000020DA File Offset: 0x000002DA
		public FastTransferContext() : base(MapiObjectType.FastTransferContext)
		{
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020EE File Offset: 0x000002EE
		public ErrorCode Configure(MapiLogon logon)
		{
			base.ParentObject = logon;
			base.Logon = logon;
			base.IsValid = true;
			return ErrorCode.NoError;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000210A File Offset: 0x0000030A
		// (set) Token: 0x06000009 RID: 9 RVA: 0x00002112 File Offset: 0x00000312
		public Microsoft.Exchange.Protocols.MAPI.Version OtherSideVersion
		{
			get
			{
				return this.otherSideVersion;
			}
			set
			{
				this.otherSideVersion = value;
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000211C File Offset: 0x0000031C
		public bool TryResolveFromNamedProperty(NamedProperty namedProperty, ref PropertyTag propertyTag)
		{
			StorePropName propName;
			if (namedProperty.Kind == NamedPropertyKind.Id)
			{
				propName = new StorePropName(namedProperty.Guid, namedProperty.Id);
			}
			else
			{
				propName = new StorePropName(namedProperty.Guid, namedProperty.Name);
			}
			ushort numberFromName = base.Logon.MapiMailbox.GetNumberFromName(base.CurrentOperationContext, true, propName, base.Logon);
			propertyTag = new PropertyTag((PropertyId)numberFromName, propertyTag.PropertyType);
			return true;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000218C File Offset: 0x0000038C
		public bool TryResolveToNamedProperty(PropertyTag propertyTag, out NamedProperty namedProperty)
		{
			StorePropName nameFromNumber = LegacyHelper.GetNameFromNumber(base.CurrentOperationContext, (ushort)propertyTag.PropertyId, base.Logon.MapiMailbox);
			if (nameFromNumber == StorePropName.Invalid)
			{
				namedProperty = new NamedProperty();
				return false;
			}
			if (nameFromNumber.Name != null)
			{
				namedProperty = new NamedProperty(nameFromNumber.Guid, nameFromNumber.Name);
			}
			else
			{
				namedProperty = new NamedProperty(nameFromNumber.Guid, nameFromNumber.DispId);
			}
			return true;
		}

		// Token: 0x04000001 RID: 1
		internal static readonly Microsoft.Exchange.Protocols.MAPI.Version OofHistorySupportMinVersion = new Microsoft.Exchange.Protocols.MAPI.Version(14, 0, 326, 0);

		// Token: 0x04000002 RID: 2
		internal static readonly Microsoft.Exchange.Protocols.MAPI.Version Dumpster2SupportMinVersion = new Microsoft.Exchange.Protocols.MAPI.Version(14, 0, 572, 0);

		// Token: 0x04000003 RID: 3
		private Microsoft.Exchange.Protocols.MAPI.Version otherSideVersion = Microsoft.Exchange.Protocols.MAPI.Version.Minimum;
	}
}
