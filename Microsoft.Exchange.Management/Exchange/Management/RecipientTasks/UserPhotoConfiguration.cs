using System;
using System.IO;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000D7B RID: 3451
	[Serializable]
	public class UserPhotoConfiguration : IConfigurable, ICmdletProxyable
	{
		// Token: 0x17002923 RID: 10531
		// (get) Token: 0x06008470 RID: 33904 RVA: 0x0021D8E6 File Offset: 0x0021BAE6
		public ObjectId Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x17002924 RID: 10532
		// (get) Token: 0x06008471 RID: 33905 RVA: 0x0021D8EE File Offset: 0x0021BAEE
		public byte[] PictureData
		{
			get
			{
				return this.pictureData;
			}
		}

		// Token: 0x17002925 RID: 10533
		// (get) Token: 0x06008472 RID: 33906 RVA: 0x0021D8F6 File Offset: 0x0021BAF6
		public int? Thumbprint
		{
			get
			{
				return this.thumbprint;
			}
		}

		// Token: 0x06008473 RID: 33907 RVA: 0x0021D8FE File Offset: 0x0021BAFE
		ValidationError[] IConfigurable.Validate()
		{
			return new ValidationError[0];
		}

		// Token: 0x06008474 RID: 33908 RVA: 0x0021D906 File Offset: 0x0021BB06
		void IConfigurable.CopyChangesFrom(IConfigurable changedObject)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06008475 RID: 33909 RVA: 0x0021D90D File Offset: 0x0021BB0D
		void IConfigurable.ResetChangeTracking()
		{
			throw new NotImplementedException();
		}

		// Token: 0x17002926 RID: 10534
		// (get) Token: 0x06008476 RID: 33910 RVA: 0x0021D914 File Offset: 0x0021BB14
		bool IConfigurable.IsValid
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002927 RID: 10535
		// (get) Token: 0x06008477 RID: 33911 RVA: 0x0021D917 File Offset: 0x0021BB17
		ObjectState IConfigurable.ObjectState
		{
			get
			{
				return ObjectState.New;
			}
		}

		// Token: 0x06008478 RID: 33912 RVA: 0x0021D91A File Offset: 0x0021BB1A
		public object GetProxyInfo()
		{
			return this.proxyInfo;
		}

		// Token: 0x06008479 RID: 33913 RVA: 0x0021D922 File Offset: 0x0021BB22
		public void SetProxyInfo(object proxyInfoValue)
		{
			if (this.proxyInfo != null && proxyInfoValue != null)
			{
				return;
			}
			this.proxyInfo = proxyInfoValue;
		}

		// Token: 0x0600847A RID: 33914 RVA: 0x0021D938 File Offset: 0x0021BB38
		internal UserPhotoConfiguration(ObjectId identity, Stream userPhotoStream, int? thumbprint)
		{
			if (identity == null)
			{
				throw new ArgumentException("identity");
			}
			if (userPhotoStream == null)
			{
				throw new ArgumentException("userPhotoStream");
			}
			this.identity = identity;
			this.thumbprint = thumbprint;
			int num = (int)userPhotoStream.Length;
			this.pictureData = new byte[num];
			userPhotoStream.Read(this.pictureData, 0, num);
		}

		// Token: 0x0400401E RID: 16414
		private byte[] pictureData;

		// Token: 0x0400401F RID: 16415
		private ObjectId identity;

		// Token: 0x04004020 RID: 16416
		private int? thumbprint;

		// Token: 0x04004021 RID: 16417
		[NonSerialized]
		private object proxyInfo;
	}
}
