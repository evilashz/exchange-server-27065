using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x02000047 RID: 71
	internal class SingleDirectoryObjectCache<TADObject, TTransformed> where TADObject : class where TTransformed : class
	{
		// Token: 0x06000265 RID: 613 RVA: 0x0000D1AC File Offset: 0x0000B3AC
		public SingleDirectoryObjectCache(Func<DateTime, bool> expirationPolicy, Func<TADObject> loadADObject, Func<TADObject, TTransformed> transformADObject)
		{
			if (expirationPolicy == null)
			{
				throw new ArgumentNullException("expirationPolicy");
			}
			if (loadADObject == null)
			{
				throw new ArgumentNullException("loadADObject");
			}
			if (transformADObject == null)
			{
				throw new ArgumentNullException("transformADObject");
			}
			this.expirationPolicy = expirationPolicy;
			this.loadADObject = loadADObject;
			this.transformADObject = transformADObject;
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000D20C File Offset: 0x0000B40C
		public void ForceReload()
		{
			lock (this.lockInstance)
			{
				this.lastRefreshTime = DateTime.MinValue;
			}
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000D254 File Offset: 0x0000B454
		public TADObject GetValues(out TTransformed transformedObject)
		{
			TADObject tadobject;
			TADObject tadobject2;
			TTransformed ttransformed;
			Exception innerException;
			lock (this.lockInstance)
			{
				tadobject = this.adObject;
				transformedObject = this.transformedObject;
				if (tadobject == null || transformedObject == null)
				{
					this.beingRefreshed = true;
					if (this.TryRefreshCache(out tadobject2, out ttransformed, out innerException))
					{
						tadobject = tadobject2;
						transformedObject = ttransformed;
						return tadobject;
					}
					throw new RpcServerException("Unable to retrieve information from AD!", RpcErrorCode.ADError, innerException);
				}
				else
				{
					if (this.expirationPolicy(this.lastRefreshTime) || this.beingRefreshed)
					{
						return tadobject;
					}
					this.beingRefreshed = true;
				}
			}
			if (this.TryRefreshCache(out tadobject2, out ttransformed, out innerException))
			{
				tadobject = tadobject2;
				transformedObject = ttransformed;
			}
			return tadobject;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000D334 File Offset: 0x0000B534
		private bool TryRefreshCache(out TADObject newADObject, out TTransformed newTransformedObject, out Exception adException)
		{
			bool flag = false;
			newADObject = default(TADObject);
			newTransformedObject = default(TTransformed);
			adException = null;
			try
			{
				newADObject = this.loadADObject();
				newTransformedObject = this.transformADObject(newADObject);
				flag = (newADObject != null && newTransformedObject != null);
			}
			catch (ADTransientException ex)
			{
				adException = ex;
			}
			catch (ADOperationException ex2)
			{
				adException = ex2;
			}
			catch (ADTopologyUnexpectedException ex3)
			{
				adException = ex3;
			}
			finally
			{
				lock (this.lockInstance)
				{
					if (flag)
					{
						this.adObject = newADObject;
						this.transformedObject = newTransformedObject;
						this.lastRefreshTime = DateTime.UtcNow;
					}
					this.beingRefreshed = false;
				}
			}
			return flag;
		}

		// Token: 0x0400013F RID: 319
		private object lockInstance = new object();

		// Token: 0x04000140 RID: 320
		private DateTime lastRefreshTime;

		// Token: 0x04000141 RID: 321
		private TADObject adObject;

		// Token: 0x04000142 RID: 322
		private TTransformed transformedObject;

		// Token: 0x04000143 RID: 323
		private Func<DateTime, bool> expirationPolicy;

		// Token: 0x04000144 RID: 324
		private Func<TADObject> loadADObject;

		// Token: 0x04000145 RID: 325
		private Func<TADObject, TTransformed> transformADObject;

		// Token: 0x04000146 RID: 326
		private bool beingRefreshed;
	}
}
