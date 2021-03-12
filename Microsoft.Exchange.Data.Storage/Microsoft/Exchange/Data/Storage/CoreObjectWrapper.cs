using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200064A RID: 1610
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class CoreObjectWrapper : ICoreObject, ICoreState, IValidatable, IDisposeTrackable, IDisposable
	{
		// Token: 0x06004296 RID: 17046 RVA: 0x0011C24B File Offset: 0x0011A44B
		internal CoreObjectWrapper(ICoreObject coreObject)
		{
			this.coreObject = coreObject;
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x06004297 RID: 17047
		public abstract DisposeTracker GetDisposeTracker();

		// Token: 0x06004298 RID: 17048 RVA: 0x0011C266 File Offset: 0x0011A466
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x1700138F RID: 5007
		// (get) Token: 0x06004299 RID: 17049 RVA: 0x0011C27B File Offset: 0x0011A47B
		public ICorePropertyBag PropertyBag
		{
			get
			{
				return this.coreObject.PropertyBag;
			}
		}

		// Token: 0x0600429A RID: 17050 RVA: 0x0011C288 File Offset: 0x0011A488
		public Schema GetCorrectSchemaForStoreObject()
		{
			return this.coreObject.GetCorrectSchemaForStoreObject();
		}

		// Token: 0x17001390 RID: 5008
		// (get) Token: 0x0600429B RID: 17051 RVA: 0x0011C295 File Offset: 0x0011A495
		public StoreSession Session
		{
			get
			{
				return this.coreObject.Session;
			}
		}

		// Token: 0x17001391 RID: 5009
		// (get) Token: 0x0600429C RID: 17052 RVA: 0x0011C2A2 File Offset: 0x0011A4A2
		public StoreObjectId StoreObjectId
		{
			get
			{
				return this.coreObject.StoreObjectId;
			}
		}

		// Token: 0x17001392 RID: 5010
		// (get) Token: 0x0600429D RID: 17053 RVA: 0x0011C2AF File Offset: 0x0011A4AF
		public StoreObjectId InternalStoreObjectId
		{
			get
			{
				return this.coreObject.InternalStoreObjectId;
			}
		}

		// Token: 0x17001393 RID: 5011
		// (get) Token: 0x0600429E RID: 17054 RVA: 0x0011C2BC File Offset: 0x0011A4BC
		public VersionedId Id
		{
			get
			{
				return this.coreObject.Id;
			}
		}

		// Token: 0x17001394 RID: 5012
		// (get) Token: 0x0600429F RID: 17055 RVA: 0x0011C2C9 File Offset: 0x0011A4C9
		// (set) Token: 0x060042A0 RID: 17056 RVA: 0x0011C2D6 File Offset: 0x0011A4D6
		public Origin Origin
		{
			get
			{
				return this.coreObject.Origin;
			}
			set
			{
				this.coreObject.Origin = value;
			}
		}

		// Token: 0x17001395 RID: 5013
		// (get) Token: 0x060042A1 RID: 17057 RVA: 0x0011C2E4 File Offset: 0x0011A4E4
		public ItemLevel ItemLevel
		{
			get
			{
				return this.coreObject.ItemLevel;
			}
		}

		// Token: 0x060042A2 RID: 17058 RVA: 0x0011C2F1 File Offset: 0x0011A4F1
		public void ResetId()
		{
			this.coreObject.ResetId();
		}

		// Token: 0x060042A3 RID: 17059 RVA: 0x0011C2FE File Offset: 0x0011A4FE
		public void SetEnableFullValidation(bool enableFullValidation)
		{
			this.coreObject.SetEnableFullValidation(enableFullValidation);
		}

		// Token: 0x17001396 RID: 5014
		// (get) Token: 0x060042A4 RID: 17060 RVA: 0x0011C30C File Offset: 0x0011A50C
		public bool IsDirty
		{
			get
			{
				return this.coreObject.IsDirty;
			}
		}

		// Token: 0x060042A5 RID: 17061 RVA: 0x0011C319 File Offset: 0x0011A519
		void IValidatable.Validate(ValidationContext context, IList<StoreObjectValidationError> validationErrors)
		{
			this.coreObject.Validate(context, validationErrors);
		}

		// Token: 0x17001397 RID: 5015
		// (get) Token: 0x060042A6 RID: 17062 RVA: 0x0011C328 File Offset: 0x0011A528
		Schema IValidatable.Schema
		{
			get
			{
				return this.coreObject.Schema;
			}
		}

		// Token: 0x17001398 RID: 5016
		// (get) Token: 0x060042A7 RID: 17063 RVA: 0x0011C335 File Offset: 0x0011A535
		bool IValidatable.ValidateAllProperties
		{
			get
			{
				return this.coreObject.ValidateAllProperties;
			}
		}

		// Token: 0x060042A8 RID: 17064 RVA: 0x0011C342 File Offset: 0x0011A542
		public void Dispose()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
			}
			this.UnadviseEvents();
		}

		// Token: 0x17001399 RID: 5017
		// (get) Token: 0x060042A9 RID: 17065 RVA: 0x0011C35D File Offset: 0x0011A55D
		protected ICoreObject CoreObject
		{
			get
			{
				return this.coreObject;
			}
		}

		// Token: 0x060042AA RID: 17066 RVA: 0x0011C365 File Offset: 0x0011A565
		protected virtual void UnadviseEvents()
		{
		}

		// Token: 0x04002491 RID: 9361
		private readonly DisposeTracker disposeTracker;

		// Token: 0x04002492 RID: 9362
		private readonly ICoreObject coreObject;
	}
}
