using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Handler
{
	// Token: 0x02000072 RID: 114
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class IcsStateStream
	{
		// Token: 0x06000489 RID: 1161 RVA: 0x00020524 File Offset: 0x0001E724
		internal IcsStateStream(IPropertyBag propertyBag)
		{
			this.propertyBag = propertyBag;
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00020534 File Offset: 0x0001E734
		public byte[] GetStateValue(PropertyTag statePropertyTag)
		{
			PropertyValue propertyValue = this.propertyBag.GetAnnotatedProperty(statePropertyTag).PropertyValue;
			if (!propertyValue.IsNotFound)
			{
				return propertyValue.GetValueAssert<byte[]>();
			}
			return Array<byte>.Empty;
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0002056C File Offset: 0x0001E76C
		public void SetStateValue(PropertyTag statePropertyTag, byte[] state)
		{
			PropertyValue property = new PropertyValue(statePropertyTag, state);
			this.propertyBag.SetProperty(property);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0002058E File Offset: 0x0001E78E
		public StorageIcsState ToXsoState()
		{
			return new StorageIcsState(this.GetStateValue(FastTransferIcsState.IdsetGiven), this.GetStateValue(FastTransferIcsState.CnsetSeen), this.GetStateValue(FastTransferIcsState.CnsetSeenAssociated), this.GetStateValue(FastTransferIcsState.CnsetRead));
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x000205C4 File Offset: 0x0001E7C4
		public void FromXsoState(StorageIcsState state)
		{
			this.SetStateValue(FastTransferIcsState.IdsetGiven, state.StateIdsetGiven);
			this.SetStateValue(FastTransferIcsState.CnsetSeen, state.StateCnsetSeen);
			this.SetStateValue(FastTransferIcsState.CnsetSeenAssociated, state.StateCnsetSeenFAI);
			this.SetStateValue(FastTransferIcsState.CnsetRead, state.StateCnsetRead);
		}

		// Token: 0x040001A8 RID: 424
		private readonly IPropertyBag propertyBag;
	}
}
