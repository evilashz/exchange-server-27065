using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using Microsoft.Runtime.Hosting;

namespace System.Reflection
{
	// Token: 0x020005F3 RID: 1523
	[ComVisible(true)]
	[Serializable]
	public class StrongNameKeyPair : IDeserializationCallback, ISerializable
	{
		// Token: 0x0600478A RID: 18314 RVA: 0x00102C2C File Offset: 0x00100E2C
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public StrongNameKeyPair(FileStream keyPairFile)
		{
			if (keyPairFile == null)
			{
				throw new ArgumentNullException("keyPairFile");
			}
			int num = (int)keyPairFile.Length;
			this._keyPairArray = new byte[num];
			keyPairFile.Read(this._keyPairArray, 0, num);
			this._keyPairExported = true;
		}

		// Token: 0x0600478B RID: 18315 RVA: 0x00102C77 File Offset: 0x00100E77
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public StrongNameKeyPair(byte[] keyPairArray)
		{
			if (keyPairArray == null)
			{
				throw new ArgumentNullException("keyPairArray");
			}
			this._keyPairArray = new byte[keyPairArray.Length];
			Array.Copy(keyPairArray, this._keyPairArray, keyPairArray.Length);
			this._keyPairExported = true;
		}

		// Token: 0x0600478C RID: 18316 RVA: 0x00102CB1 File Offset: 0x00100EB1
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public StrongNameKeyPair(string keyPairContainer)
		{
			if (keyPairContainer == null)
			{
				throw new ArgumentNullException("keyPairContainer");
			}
			this._keyPairContainer = keyPairContainer;
			this._keyPairExported = false;
		}

		// Token: 0x0600478D RID: 18317 RVA: 0x00102CD8 File Offset: 0x00100ED8
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected StrongNameKeyPair(SerializationInfo info, StreamingContext context)
		{
			this._keyPairExported = (bool)info.GetValue("_keyPairExported", typeof(bool));
			this._keyPairArray = (byte[])info.GetValue("_keyPairArray", typeof(byte[]));
			this._keyPairContainer = (string)info.GetValue("_keyPairContainer", typeof(string));
			this._publicKey = (byte[])info.GetValue("_publicKey", typeof(byte[]));
		}

		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x0600478E RID: 18318 RVA: 0x00102D6C File Offset: 0x00100F6C
		public byte[] PublicKey
		{
			[SecuritySafeCritical]
			get
			{
				if (this._publicKey == null)
				{
					this._publicKey = this.ComputePublicKey();
				}
				byte[] array = new byte[this._publicKey.Length];
				Array.Copy(this._publicKey, array, this._publicKey.Length);
				return array;
			}
		}

		// Token: 0x0600478F RID: 18319 RVA: 0x00102DB0 File Offset: 0x00100FB0
		[SecurityCritical]
		private unsafe byte[] ComputePublicKey()
		{
			byte[] array = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				IntPtr zero = IntPtr.Zero;
				int num = 0;
				try
				{
					bool flag;
					if (this._keyPairExported)
					{
						flag = StrongNameHelpers.StrongNameGetPublicKey(null, this._keyPairArray, this._keyPairArray.Length, out zero, out num);
					}
					else
					{
						flag = StrongNameHelpers.StrongNameGetPublicKey(this._keyPairContainer, null, 0, out zero, out num);
					}
					if (!flag)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_StrongNameGetPublicKey"));
					}
					array = new byte[num];
					Buffer.Memcpy(array, 0, (byte*)zero.ToPointer(), 0, num);
				}
				finally
				{
					if (zero != IntPtr.Zero)
					{
						StrongNameHelpers.StrongNameFreeBuffer(zero);
					}
				}
			}
			return array;
		}

		// Token: 0x06004790 RID: 18320 RVA: 0x00102E64 File Offset: 0x00101064
		[SecurityCritical]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("_keyPairExported", this._keyPairExported);
			info.AddValue("_keyPairArray", this._keyPairArray);
			info.AddValue("_keyPairContainer", this._keyPairContainer);
			info.AddValue("_publicKey", this._publicKey);
		}

		// Token: 0x06004791 RID: 18321 RVA: 0x00102EB5 File Offset: 0x001010B5
		void IDeserializationCallback.OnDeserialization(object sender)
		{
		}

		// Token: 0x06004792 RID: 18322 RVA: 0x00102EB7 File Offset: 0x001010B7
		private bool GetKeyPair(out object arrayOrContainer)
		{
			arrayOrContainer = (this._keyPairExported ? this._keyPairArray : this._keyPairContainer);
			return this._keyPairExported;
		}

		// Token: 0x04001D61 RID: 7521
		private bool _keyPairExported;

		// Token: 0x04001D62 RID: 7522
		private byte[] _keyPairArray;

		// Token: 0x04001D63 RID: 7523
		private string _keyPairContainer;

		// Token: 0x04001D64 RID: 7524
		private byte[] _publicKey;
	}
}
