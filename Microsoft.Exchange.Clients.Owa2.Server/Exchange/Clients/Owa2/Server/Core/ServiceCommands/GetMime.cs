using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.ServiceCommands
{
	// Token: 0x0200036E RID: 878
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class GetMime : ServiceCommand<string>
	{
		// Token: 0x06001C32 RID: 7218 RVA: 0x0006FF12 File Offset: 0x0006E112
		public GetMime(CallContext callContext, ItemId itemId) : base(callContext)
		{
			this.itemId = itemId;
		}

		// Token: 0x06001C33 RID: 7219 RVA: 0x0006FF24 File Offset: 0x0006E124
		protected override string InternalExecute()
		{
			IdAndSession idAndSession = base.IdConverter.ConvertItemIdToIdAndSessionReadOnly(this.itemId);
			MessageItem messageItem = Item.BindAsMessage(idAndSession.Session, idAndSession.Id);
			messageItem.Load(StoreObjectSchema.ContentConversionProperties);
			OutboundConversionOptions outboundConversionOptions = new OutboundConversionOptions(base.CallContext.DefaultDomain.DomainName.ToString());
			UserContext userContext = UserContextManager.GetUserContext(base.CallContext.HttpContext);
			outboundConversionOptions.UserADSession = UserContextUtilities.CreateADRecipientSession(base.CallContext.ClientCulture.LCID, true, ConsistencyMode.IgnoreInvalid, false, userContext, true, base.CallContext.Budget);
			outboundConversionOptions.LoadPerOrganizationCharsetDetectionOptions(userContext.ExchangePrincipal.MailboxInfo.OrganizationId);
			outboundConversionOptions.AllowPartialStnefConversion = true;
			outboundConversionOptions.DemoteBcc = true;
			string @string;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				ItemConversion.ConvertItemToMime(messageItem, memoryStream, outboundConversionOptions);
				memoryStream.Position = 0L;
				byte[] array = new byte[memoryStream.Length];
				memoryStream.Read(array, 0, array.Length);
				@string = Encoding.ASCII.GetString(array);
			}
			return @string;
		}

		// Token: 0x04000FF6 RID: 4086
		private ItemId itemId;
	}
}
