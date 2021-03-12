using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.PST;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.FastTransfer;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000012 RID: 18
	internal class PSTSession : ISession
	{
		// Token: 0x060000E5 RID: 229 RVA: 0x00006658 File Offset: 0x00004858
		public PSTSession(PstMailbox pstMailbox)
		{
			this.pstMailbox = pstMailbox;
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00006667 File Offset: 0x00004867
		public PstMailbox PstMailbox
		{
			get
			{
				return this.pstMailbox;
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00006670 File Offset: 0x00004870
		public bool TryResolveToNamedProperty(PropertyTag propertyTag, out NamedProperty namedProperty)
		{
			namedProperty = null;
			INamedProperty namedProperty2 = null;
			if (!propertyTag.IsNamedProperty || !this.pstMailbox.IPst.ReadNamedPropertyTable().TryGetValue((ushort)propertyTag.PropertyId, out namedProperty2))
			{
				return false;
			}
			try
			{
				namedProperty = (namedProperty2.IsString ? new NamedProperty(namedProperty2.Guid, namedProperty2.Name) : new NamedProperty(namedProperty2.Guid, namedProperty2.Id));
			}
			catch (ArgumentException ex)
			{
				throw new ExArgumentException(ex.Message, ex);
			}
			return true;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000066FC File Offset: 0x000048FC
		public bool TryResolveFromNamedProperty(NamedProperty namedProperty, ref PropertyTag propertyTag)
		{
			ushort num;
			try
			{
				if (namedProperty.Kind == NamedPropertyKind.String)
				{
					num = this.pstMailbox.IPst.ReadIdFromNamedProp(namedProperty.Name, 0U, namedProperty.Guid, true);
				}
				else
				{
					num = this.pstMailbox.IPst.ReadIdFromNamedProp(null, namedProperty.Id, namedProperty.Guid, true);
				}
			}
			catch (PSTExceptionBase innerException)
			{
				throw new MailboxReplicationPermanentException(new LocalizedString(this.pstMailbox.IPst.FileName), innerException);
			}
			if (num != 0)
			{
				propertyTag = new PropertyTag((PropertyId)num, propertyTag.PropertyType);
				return true;
			}
			propertyTag = PropertyTag.CreateError(propertyTag.PropertyId);
			return false;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000067AC File Offset: 0x000049AC
		public void Dispose()
		{
		}

		// Token: 0x04000043 RID: 67
		private PstMailbox pstMailbox;
	}
}
