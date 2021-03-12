using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200064B RID: 1611
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class CoreItemWrapper : CoreObjectWrapper, ICoreItem, ICoreObject, ICoreState, IValidatable, IDisposeTrackable, IDisposable, ILocationIdentifierController
	{
		// Token: 0x060042AB RID: 17067 RVA: 0x0011C367 File Offset: 0x0011A567
		internal CoreItemWrapper(ICoreItem coreItem) : base(coreItem)
		{
		}

		// Token: 0x060042AC RID: 17068 RVA: 0x0011C370 File Offset: 0x0011A570
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<CoreItemWrapper>(this);
		}

		// Token: 0x060042AD RID: 17069 RVA: 0x0011C378 File Offset: 0x0011A578
		protected override void UnadviseEvents()
		{
			this.CoreItem.BeforeSend -= this.beforeSendEventHandler;
			this.beforeSendEventHandler = null;
			base.UnadviseEvents();
		}

		// Token: 0x1700139A RID: 5018
		// (get) Token: 0x060042AE RID: 17070 RVA: 0x0011C398 File Offset: 0x0011A598
		private ICoreItem CoreItem
		{
			get
			{
				return (ICoreItem)base.CoreObject;
			}
		}

		// Token: 0x1700139B RID: 5019
		// (get) Token: 0x060042AF RID: 17071 RVA: 0x0011C3A5 File Offset: 0x0011A5A5
		public CoreRecipientCollection Recipients
		{
			get
			{
				return this.CoreItem.Recipients;
			}
		}

		// Token: 0x1700139C RID: 5020
		// (get) Token: 0x060042B0 RID: 17072 RVA: 0x0011C3B2 File Offset: 0x0011A5B2
		public bool IsReadOnly
		{
			get
			{
				return this.CoreItem.IsReadOnly;
			}
		}

		// Token: 0x1700139D RID: 5021
		// (get) Token: 0x060042B1 RID: 17073 RVA: 0x0011C3BF File Offset: 0x0011A5BF
		// (set) Token: 0x060042B2 RID: 17074 RVA: 0x0011C3CC File Offset: 0x0011A5CC
		public ICoreItem TopLevelItem
		{
			get
			{
				return this.CoreItem.TopLevelItem;
			}
			set
			{
				this.CoreItem.TopLevelItem = value;
			}
		}

		// Token: 0x1700139E RID: 5022
		// (get) Token: 0x060042B3 RID: 17075 RVA: 0x0011C3DA File Offset: 0x0011A5DA
		public CoreAttachmentCollection AttachmentCollection
		{
			get
			{
				return this.CoreItem.AttachmentCollection;
			}
		}

		// Token: 0x060042B4 RID: 17076 RVA: 0x0011C3E7 File Offset: 0x0011A5E7
		CoreRecipientCollection ICoreItem.GetRecipientCollection(bool forceOpen)
		{
			return this.CoreItem.GetRecipientCollection(forceOpen);
		}

		// Token: 0x1700139F RID: 5023
		// (get) Token: 0x060042B5 RID: 17077 RVA: 0x0011C3F5 File Offset: 0x0011A5F5
		MapiMessage ICoreItem.MapiMessage
		{
			get
			{
				return this.CoreItem.MapiMessage;
			}
		}

		// Token: 0x060042B6 RID: 17078 RVA: 0x0011C402 File Offset: 0x0011A602
		void ICoreItem.OpenAttachmentCollection(ICoreItem coreItem)
		{
			this.CoreItem.OpenAttachmentCollection(coreItem);
		}

		// Token: 0x060042B7 RID: 17079 RVA: 0x0011C410 File Offset: 0x0011A610
		void ICoreItem.OpenAttachmentCollection()
		{
			this.CoreItem.OpenAttachmentCollection();
		}

		// Token: 0x060042B8 RID: 17080 RVA: 0x0011C41D File Offset: 0x0011A61D
		void ICoreItem.DisposeAttachmentCollection()
		{
			this.CoreItem.DisposeAttachmentCollection();
		}

		// Token: 0x060042B9 RID: 17081 RVA: 0x0011C42A File Offset: 0x0011A62A
		void ICoreItem.OpenAsReadWrite()
		{
			this.CoreItem.OpenAsReadWrite();
		}

		// Token: 0x060042BA RID: 17082 RVA: 0x0011C437 File Offset: 0x0011A637
		ConflictResolutionResult ICoreItem.Save(SaveMode saveMode)
		{
			return this.CoreItem.Save(saveMode);
		}

		// Token: 0x060042BB RID: 17083 RVA: 0x0011C445 File Offset: 0x0011A645
		ConflictResolutionResult ICoreItem.Flush(SaveMode saveMode)
		{
			return this.CoreItem.Flush(saveMode);
		}

		// Token: 0x060042BC RID: 17084 RVA: 0x0011C453 File Offset: 0x0011A653
		ConflictResolutionResult ICoreItem.InternalFlush(SaveMode saveMode, CallbackContext callbackContext)
		{
			return this.CoreItem.InternalFlush(saveMode, callbackContext);
		}

		// Token: 0x060042BD RID: 17085 RVA: 0x0011C462 File Offset: 0x0011A662
		ConflictResolutionResult ICoreItem.InternalFlush(SaveMode saveMode, CoreItemOperation operation, CallbackContext callbackContext)
		{
			return this.CoreItem.InternalFlush(saveMode, operation, callbackContext);
		}

		// Token: 0x060042BE RID: 17086 RVA: 0x0011C472 File Offset: 0x0011A672
		ConflictResolutionResult ICoreItem.InternalSave(SaveMode saveMode, CallbackContext callbackContext)
		{
			return this.CoreItem.InternalSave(saveMode, callbackContext);
		}

		// Token: 0x060042BF RID: 17087 RVA: 0x0011C481 File Offset: 0x0011A681
		void ICoreItem.SaveRecipients()
		{
			this.CoreItem.SaveRecipients();
		}

		// Token: 0x060042C0 RID: 17088 RVA: 0x0011C48E File Offset: 0x0011A68E
		void ICoreItem.AbandonRecipientChanges()
		{
			this.CoreItem.AbandonRecipientChanges();
		}

		// Token: 0x060042C1 RID: 17089 RVA: 0x0011C49B File Offset: 0x0011A69B
		void ICoreItem.Submit()
		{
			this.CoreItem.Submit();
		}

		// Token: 0x060042C2 RID: 17090 RVA: 0x0011C4A8 File Offset: 0x0011A6A8
		void ICoreItem.Submit(SubmitMessageFlags submitFlags)
		{
			this.CoreItem.Submit(submitFlags);
		}

		// Token: 0x060042C3 RID: 17091 RVA: 0x0011C4B6 File Offset: 0x0011A6B6
		void ICoreItem.TransportSend(out PropertyDefinition[] properties, out object[] values)
		{
			this.CoreItem.TransportSend(out properties, out values);
		}

		// Token: 0x060042C4 RID: 17092 RVA: 0x0011C4C5 File Offset: 0x0011A6C5
		PropertyError[] ICoreItem.CopyItem(ICoreItem destinationItem, CopyPropertiesFlags copyPropertiesFlags, CopySubObjects copySubObjects, NativeStorePropertyDefinition[] excludeProperties)
		{
			return this.CoreItem.CopyItem(destinationItem, copyPropertiesFlags, copySubObjects, excludeProperties);
		}

		// Token: 0x060042C5 RID: 17093 RVA: 0x0011C4D7 File Offset: 0x0011A6D7
		PropertyError[] ICoreItem.CopyProperties(ICoreItem destinationItem, CopyPropertiesFlags copyPropertiesFlags, NativeStorePropertyDefinition[] includeProperties)
		{
			return this.CoreItem.CopyProperties(destinationItem, copyPropertiesFlags, includeProperties);
		}

		// Token: 0x170013A0 RID: 5024
		// (get) Token: 0x060042C6 RID: 17094 RVA: 0x0011C4E7 File Offset: 0x0011A6E7
		bool ICoreItem.IsAttachmentCollectionLoaded
		{
			get
			{
				return this.CoreItem.IsAttachmentCollectionLoaded;
			}
		}

		// Token: 0x060042C7 RID: 17095 RVA: 0x0011C4F4 File Offset: 0x0011A6F4
		void ICoreItem.Reload()
		{
			this.CoreItem.Reload();
		}

		// Token: 0x170013A1 RID: 5025
		// (get) Token: 0x060042C8 RID: 17096 RVA: 0x0011C501 File Offset: 0x0011A701
		bool ICoreItem.AreOptionalAutoloadPropertiesLoaded
		{
			get
			{
				return this.CoreItem.AreOptionalAutoloadPropertiesLoaded;
			}
		}

		// Token: 0x060042C9 RID: 17097 RVA: 0x0011C50E File Offset: 0x0011A70E
		void ICoreItem.SetIrresolvableChange()
		{
			this.CoreItem.SetIrresolvableChange();
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060042CA RID: 17098 RVA: 0x0011C51B File Offset: 0x0011A71B
		// (remove) Token: 0x060042CB RID: 17099 RVA: 0x0011C540 File Offset: 0x0011A740
		event Action ICoreItem.BeforeSend
		{
			add
			{
				this.beforeSendEventHandler = (Action)Delegate.Combine(this.beforeSendEventHandler, value);
				this.CoreItem.BeforeSend += value;
			}
			remove
			{
				this.beforeSendEventHandler = (Action)Delegate.Remove(this.beforeSendEventHandler, value);
				this.CoreItem.BeforeSend -= value;
			}
		}

		// Token: 0x170013A2 RID: 5026
		// (get) Token: 0x060042CC RID: 17100 RVA: 0x0011C565 File Offset: 0x0011A765
		// (set) Token: 0x060042CD RID: 17101 RVA: 0x0011C572 File Offset: 0x0011A772
		PropertyBagSaveFlags ICoreItem.SaveFlags
		{
			get
			{
				return this.CoreItem.SaveFlags;
			}
			set
			{
				this.CoreItem.SaveFlags = value;
			}
		}

		// Token: 0x170013A3 RID: 5027
		// (get) Token: 0x060042CE RID: 17102 RVA: 0x0011C580 File Offset: 0x0011A780
		Body ICoreItem.Body
		{
			get
			{
				return this.CoreItem.Body;
			}
		}

		// Token: 0x170013A4 RID: 5028
		// (get) Token: 0x060042CF RID: 17103 RVA: 0x0011C58D File Offset: 0x0011A78D
		ItemCharsetDetector ICoreItem.CharsetDetector
		{
			get
			{
				return this.CoreItem.CharsetDetector;
			}
		}

		// Token: 0x170013A5 RID: 5029
		// (set) Token: 0x060042D0 RID: 17104 RVA: 0x0011C59A File Offset: 0x0011A79A
		int ICoreItem.PreferredInternetCodePageForShiftJis
		{
			set
			{
				this.CoreItem.PreferredInternetCodePageForShiftJis = value;
			}
		}

		// Token: 0x170013A6 RID: 5030
		// (set) Token: 0x060042D1 RID: 17105 RVA: 0x0011C5A8 File Offset: 0x0011A7A8
		int ICoreItem.RequiredCoverage
		{
			set
			{
				this.CoreItem.RequiredCoverage = value;
			}
		}

		// Token: 0x060042D2 RID: 17106 RVA: 0x0011C5B6 File Offset: 0x0011A7B6
		void ICoreItem.GetCharsetDetectionData(StringBuilder stringBuilder, CharsetDetectionDataFlags flags)
		{
			this.CoreItem.GetCharsetDetectionData(stringBuilder, flags);
		}

		// Token: 0x060042D3 RID: 17107 RVA: 0x0011C5C5 File Offset: 0x0011A7C5
		void ICoreItem.SetCoreItemContext(ICoreItemContext context)
		{
			this.CoreItem.SetCoreItemContext(context);
		}

		// Token: 0x170013A7 RID: 5031
		// (get) Token: 0x060042D4 RID: 17108 RVA: 0x0011C5D3 File Offset: 0x0011A7D3
		LocationIdentifierHelper ILocationIdentifierController.LocationIdentifierHelperInstance
		{
			get
			{
				return this.CoreItem.LocationIdentifierHelperInstance;
			}
		}

		// Token: 0x060042D5 RID: 17109 RVA: 0x0011C5E0 File Offset: 0x0011A7E0
		public void SetReadFlag(int flags, bool deferErrors)
		{
			this.CoreItem.SetReadFlag(flags, deferErrors);
		}

		// Token: 0x04002493 RID: 9363
		private Action beforeSendEventHandler;
	}
}
