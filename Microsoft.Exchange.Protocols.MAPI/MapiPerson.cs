using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.Mapi;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Server.Storage.LogicalDataModel;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000077 RID: 119
	public sealed class MapiPerson : MapiPropBagBase
	{
		// Token: 0x06000386 RID: 902 RVA: 0x0001BF34 File Offset: 0x0001A134
		internal MapiPerson() : base(MapiObjectType.Person)
		{
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000387 RID: 903 RVA: 0x0001BF3D File Offset: 0x0001A13D
		protected override PropertyBag StorePropertyBag
		{
			get
			{
				return this.StorePerson;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000388 RID: 904 RVA: 0x0001BF45 File Offset: 0x0001A145
		internal Recipient StorePerson
		{
			get
			{
				base.ThrowIfNotValid(null);
				return this.storePerson;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000389 RID: 905 RVA: 0x0001BF54 File Offset: 0x0001A154
		internal bool IsDeleted
		{
			get
			{
				base.ThrowIfNotValid(null);
				return this.storePerson == null;
			}
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0001BF66 File Offset: 0x0001A166
		public static StorePropTag[] GetRecipientPropListStandard()
		{
			return MapiPerson.recipientPropListStandard;
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0001BF6D File Offset: 0x0001A16D
		public int GetRowId()
		{
			base.ThrowIfNotValid(null);
			return this.rowId;
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0001BF7C File Offset: 0x0001A17C
		public void Delete()
		{
			base.ThrowIfNotValid(null);
			this.parentMapiObject.GetRecipients().GetRecipientCollection().Remove(this.storePerson);
			this.storePerson = null;
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0001BFA7 File Offset: 0x0001A1A7
		internal static int GetRecipientPropListStandardCount(MapiMessage messageBase)
		{
			if (messageBase == null || !messageBase.IsValid)
			{
				throw new ExExceptionInvalidParameter((LID)61752U, "Null or invalid 'messageBase' parameter in 'GetRecipientPropListStandardCount' method.");
			}
			return MapiPerson.recipientPropListStandard.Length;
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0001BFD0 File Offset: 0x0001A1D0
		internal void Configure(MapiMessage message, int rowId, Recipient storePerson)
		{
			if (base.IsDisposed)
			{
				ExTraceGlobals.GeneralTracer.TraceError(0L, "Configure called on a Dispose'd MapiPerson!  Throwing ExExceptionInvalidObject!");
				throw new ExExceptionInvalidObject((LID)37176U, "Configure cannot be invoked after Dispose.");
			}
			if (base.IsValid)
			{
				ExTraceGlobals.GeneralTracer.TraceError(0L, "Configure called on already configured MapiPerson!  Throwing ExExceptionInvalidObject!");
				throw new ExExceptionInvalidObject((LID)53560U, "Object has already been Configure'd");
			}
			this.parentMapiObject = message;
			if (this.parentMapiObject == null || !this.parentMapiObject.IsValid)
			{
				ExTraceGlobals.GeneralTracer.TraceError(0L, "messageBase is null or invalid, throwing ExExceptionInvalidParameter");
				throw new ExExceptionInvalidParameter((LID)41272U, "Expected valid messageBase");
			}
			this.rowId = rowId;
			this.storePerson = storePerson;
			base.Logon = this.parentMapiObject.Logon;
			base.IsValid = true;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0001C0A4 File Offset: 0x0001A2A4
		protected override bool TryGetPropertyImp(MapiContext context, ushort propId, out StorePropTag actualPropTag, out object propValue)
		{
			if (propId == 12288)
			{
				propValue = this.GetRowId();
				actualPropTag = PropTag.Recipient.RowId;
				return true;
			}
			return base.TryGetPropertyImp(context, propId, out actualPropTag, out propValue);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0001C0E4 File Offset: 0x0001A2E4
		protected override object GetPropertyValueImp(MapiContext context, StorePropTag propTag)
		{
			ushort propId = propTag.PropId;
			object result;
			if (propId == 12288)
			{
				result = ((propTag != PropTag.Recipient.RowId) ? null : this.GetRowId());
			}
			else
			{
				result = base.GetPropertyValueImp(context, propTag);
			}
			return result;
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0001C129 File Offset: 0x0001A329
		public int GetRecipientType()
		{
			base.ThrowIfNotValid(null);
			return (int)this.StorePerson.GetPropertyValue(base.CurrentOperationContext, PropTag.Recipient.RecipientType);
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0001C14D File Offset: 0x0001A34D
		internal void SetRecipientType(int recipientType)
		{
			base.ThrowIfNotValid(null);
			this.StorePerson.SetProperty(base.CurrentOperationContext, PropTag.Recipient.RecipientType, recipientType);
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0001C173 File Offset: 0x0001A373
		internal string GetDisplayName()
		{
			base.ThrowIfNotValid(null);
			return (string)this.StorePerson.GetPropertyValue(base.CurrentOperationContext, PropTag.Recipient.DisplayName);
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0001C197 File Offset: 0x0001A397
		internal string GetTransmitableDisplayName()
		{
			base.ThrowIfNotValid(null);
			return (string)this.StorePerson.GetPropertyValue(base.CurrentOperationContext, PropTag.Recipient.TransmitableDisplayName);
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0001C1BB File Offset: 0x0001A3BB
		internal string GetSimpleDisplayName()
		{
			base.ThrowIfNotValid(null);
			return (string)this.StorePerson.GetPropertyValue(base.CurrentOperationContext, PropTag.Recipient.SimpleDisplayName);
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0001C1DF File Offset: 0x0001A3DF
		internal byte[] GetEntryId()
		{
			base.ThrowIfNotValid(null);
			return (byte[])this.StorePerson.GetPropertyValue(base.CurrentOperationContext, PropTag.Recipient.EntryId);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0001C203 File Offset: 0x0001A403
		internal string GetAddrType()
		{
			base.ThrowIfNotValid(null);
			return (string)this.StorePerson.GetPropertyValue(base.CurrentOperationContext, PropTag.Recipient.AddressType);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0001C227 File Offset: 0x0001A427
		internal string GetEmailAddress()
		{
			base.ThrowIfNotValid(null);
			return (string)this.StorePerson.GetPropertyValue(base.CurrentOperationContext, PropTag.Recipient.EmailAddress);
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0001C24B File Offset: 0x0001A44B
		internal bool GetResponsibility()
		{
			base.ThrowIfNotValid(null);
			return (bool)this.StorePerson.GetPropertyValue(base.CurrentOperationContext, PropTag.Recipient.Responsibility);
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0001C26F File Offset: 0x0001A46F
		internal void SetResponsibility(bool value)
		{
			base.ThrowIfNotValid(null);
			this.StorePerson.SetProperty(base.CurrentOperationContext, PropTag.Recipient.Responsibility, value);
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0001C295 File Offset: 0x0001A495
		internal bool GetSendRichInfo()
		{
			base.ThrowIfNotValid(null);
			return (bool)this.StorePerson.GetPropertyValue(base.CurrentOperationContext, PropTag.Recipient.SendRichInfo);
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0001C2B9 File Offset: 0x0001A4B9
		internal int GetDisplayType()
		{
			base.ThrowIfNotValid(null);
			return (int)this.StorePerson.GetPropertyValue(base.CurrentOperationContext, PropTag.Recipient.DisplayType);
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0001C2DD File Offset: 0x0001A4DD
		internal byte[] GetSearchKey()
		{
			base.ThrowIfNotValid(null);
			return (byte[])this.StorePerson.GetPropertyValue(base.CurrentOperationContext, PropTag.Recipient.SearchKey);
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0001C304 File Offset: 0x0001A504
		internal void SaveChanges(MapiContext context)
		{
			base.ThrowIfNotValid(null);
			if (this.IsDeleted)
			{
				return;
			}
			if (string.Compare("SMTP", this.GetAddrType(), StringComparison.OrdinalIgnoreCase) == 0)
			{
				string value = this.StorePropertyBag.GetPropertyValue(context, PropTag.Recipient.SmtpAddress) as string;
				if (string.IsNullOrEmpty(value))
				{
					this.StorePerson.SetProperty(context, PropTag.Recipient.SmtpAddress, this.GetEmailAddress());
				}
			}
			base.CommitDirtyStreams(context);
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0001C372 File Offset: 0x0001A572
		internal override void CheckRights(MapiContext context, FolderSecurity.ExchangeSecurityDescriptorFolderRights requestedRights, bool allRights, AccessCheckOperation operation, LID lid)
		{
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0001C374 File Offset: 0x0001A574
		internal override void CheckPropertyRights(MapiContext context, FolderSecurity.ExchangeSecurityDescriptorFolderRights requestedRights, AccessCheckOperation operation, LID lid)
		{
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0001C390 File Offset: 0x0001A590
		internal void CollectExtraProperties(MapiContext context, HashSet<StorePropTag> extraProperties)
		{
			this.StorePropertyBag.EnumerateProperties(context, delegate(StorePropTag propTag, object propValue)
			{
				extraProperties.Add(propTag);
				return true;
			}, false);
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0001C3C3 File Offset: 0x0001A5C3
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MapiPerson>(this);
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0001C3CB File Offset: 0x0001A5CB
		protected override void InternalDispose(bool calledFromDispose)
		{
			this.storePerson = null;
			this.rowId = -1;
			this.parentMapiObject = null;
			base.InternalDispose(calledFromDispose);
		}

		// Token: 0x04000259 RID: 601
		private static StorePropTag[] recipientPropListStandard = new StorePropTag[]
		{
			PropTag.Recipient.DisplayName,
			PropTag.Recipient.AddressType,
			PropTag.Recipient.EmailAddress,
			PropTag.Recipient.RowId,
			PropTag.Recipient.InstanceKey,
			PropTag.Recipient.RecipientType,
			PropTag.Recipient.EntryId,
			PropTag.Recipient.SearchKey,
			PropTag.Recipient.TransmitableDisplayName,
			PropTag.Recipient.Responsibility,
			PropTag.Recipient.SendRichInfo
		};

		// Token: 0x0400025A RID: 602
		private MapiMessage parentMapiObject;

		// Token: 0x0400025B RID: 603
		private Recipient storePerson;

		// Token: 0x0400025C RID: 604
		private int rowId;
	}
}
