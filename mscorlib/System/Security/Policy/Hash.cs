using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Security.Util;

namespace System.Security.Policy
{
	// Token: 0x02000348 RID: 840
	[ComVisible(true)]
	[Serializable]
	public sealed class Hash : EvidenceBase, ISerializable
	{
		// Token: 0x06002AAE RID: 10926 RVA: 0x0009E5BC File Offset: 0x0009C7BC
		[SecurityCritical]
		internal Hash(SerializationInfo info, StreamingContext context)
		{
			Dictionary<Type, byte[]> dictionary = info.GetValueNoThrow("Hashes", typeof(Dictionary<Type, byte[]>)) as Dictionary<Type, byte[]>;
			if (dictionary != null)
			{
				this.m_hashes = dictionary;
				return;
			}
			this.m_hashes = new Dictionary<Type, byte[]>();
			byte[] array = info.GetValueNoThrow("Md5", typeof(byte[])) as byte[];
			if (array != null)
			{
				this.m_hashes[typeof(MD5)] = array;
			}
			byte[] array2 = info.GetValueNoThrow("Sha1", typeof(byte[])) as byte[];
			if (array2 != null)
			{
				this.m_hashes[typeof(SHA1)] = array2;
			}
			byte[] array3 = info.GetValueNoThrow("RawData", typeof(byte[])) as byte[];
			if (array3 != null)
			{
				this.GenerateDefaultHashes(array3);
			}
		}

		// Token: 0x06002AAF RID: 10927 RVA: 0x0009E690 File Offset: 0x0009C890
		public Hash(Assembly assembly)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			if (assembly.IsDynamic)
			{
				throw new ArgumentException(Environment.GetResourceString("Security_CannotGenerateHash"), "assembly");
			}
			this.m_hashes = new Dictionary<Type, byte[]>();
			this.m_assembly = (assembly as RuntimeAssembly);
			if (this.m_assembly == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeAssembly"), "assembly");
			}
		}

		// Token: 0x06002AB0 RID: 10928 RVA: 0x0009E70E File Offset: 0x0009C90E
		private Hash(Hash hash)
		{
			this.m_assembly = hash.m_assembly;
			this.m_rawData = hash.m_rawData;
			this.m_hashes = new Dictionary<Type, byte[]>(hash.m_hashes);
		}

		// Token: 0x06002AB1 RID: 10929 RVA: 0x0009E740 File Offset: 0x0009C940
		private Hash(Type hashType, byte[] hashValue)
		{
			this.m_hashes = new Dictionary<Type, byte[]>();
			byte[] array = new byte[hashValue.Length];
			Array.Copy(hashValue, array, array.Length);
			this.m_hashes[hashType] = hashValue;
		}

		// Token: 0x06002AB2 RID: 10930 RVA: 0x0009E77E File Offset: 0x0009C97E
		public static Hash CreateSHA1(byte[] sha1)
		{
			if (sha1 == null)
			{
				throw new ArgumentNullException("sha1");
			}
			return new Hash(typeof(SHA1), sha1);
		}

		// Token: 0x06002AB3 RID: 10931 RVA: 0x0009E79E File Offset: 0x0009C99E
		public static Hash CreateSHA256(byte[] sha256)
		{
			if (sha256 == null)
			{
				throw new ArgumentNullException("sha256");
			}
			return new Hash(typeof(SHA256), sha256);
		}

		// Token: 0x06002AB4 RID: 10932 RVA: 0x0009E7BE File Offset: 0x0009C9BE
		public static Hash CreateMD5(byte[] md5)
		{
			if (md5 == null)
			{
				throw new ArgumentNullException("md5");
			}
			return new Hash(typeof(MD5), md5);
		}

		// Token: 0x06002AB5 RID: 10933 RVA: 0x0009E7DE File Offset: 0x0009C9DE
		public override EvidenceBase Clone()
		{
			return new Hash(this);
		}

		// Token: 0x06002AB6 RID: 10934 RVA: 0x0009E7E6 File Offset: 0x0009C9E6
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
			this.GenerateDefaultHashes();
		}

		// Token: 0x06002AB7 RID: 10935 RVA: 0x0009E7F0 File Offset: 0x0009C9F0
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			this.GenerateDefaultHashes();
			byte[] value;
			if (this.m_hashes.TryGetValue(typeof(MD5), out value))
			{
				info.AddValue("Md5", value);
			}
			byte[] value2;
			if (this.m_hashes.TryGetValue(typeof(SHA1), out value2))
			{
				info.AddValue("Sha1", value2);
			}
			info.AddValue("RawData", null);
			info.AddValue("PEFile", IntPtr.Zero);
			info.AddValue("Hashes", this.m_hashes);
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06002AB8 RID: 10936 RVA: 0x0009E880 File Offset: 0x0009CA80
		public byte[] SHA1
		{
			get
			{
				byte[] array = null;
				if (!this.m_hashes.TryGetValue(typeof(SHA1), out array))
				{
					array = this.GenerateHash(Hash.GetDefaultHashImplementationOrFallback(typeof(SHA1), typeof(SHA1)));
				}
				byte[] array2 = new byte[array.Length];
				Array.Copy(array, array2, array2.Length);
				return array2;
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06002AB9 RID: 10937 RVA: 0x0009E8DC File Offset: 0x0009CADC
		public byte[] SHA256
		{
			get
			{
				byte[] array = null;
				if (!this.m_hashes.TryGetValue(typeof(SHA256), out array))
				{
					array = this.GenerateHash(Hash.GetDefaultHashImplementationOrFallback(typeof(SHA256), typeof(SHA256)));
				}
				byte[] array2 = new byte[array.Length];
				Array.Copy(array, array2, array2.Length);
				return array2;
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06002ABA RID: 10938 RVA: 0x0009E938 File Offset: 0x0009CB38
		public byte[] MD5
		{
			get
			{
				byte[] array = null;
				if (!this.m_hashes.TryGetValue(typeof(MD5), out array))
				{
					array = this.GenerateHash(Hash.GetDefaultHashImplementationOrFallback(typeof(MD5), typeof(MD5)));
				}
				byte[] array2 = new byte[array.Length];
				Array.Copy(array, array2, array2.Length);
				return array2;
			}
		}

		// Token: 0x06002ABB RID: 10939 RVA: 0x0009E994 File Offset: 0x0009CB94
		public byte[] GenerateHash(HashAlgorithm hashAlg)
		{
			if (hashAlg == null)
			{
				throw new ArgumentNullException("hashAlg");
			}
			byte[] array = this.GenerateHash(hashAlg.GetType());
			byte[] array2 = new byte[array.Length];
			Array.Copy(array, array2, array2.Length);
			return array2;
		}

		// Token: 0x06002ABC RID: 10940 RVA: 0x0009E9D0 File Offset: 0x0009CBD0
		private byte[] GenerateHash(Type hashType)
		{
			Type hashIndexType = Hash.GetHashIndexType(hashType);
			byte[] array = null;
			if (!this.m_hashes.TryGetValue(hashIndexType, out array))
			{
				if (this.m_assembly == null)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Security_CannotGenerateHash"));
				}
				array = Hash.GenerateHash(hashType, this.GetRawData());
				this.m_hashes[hashIndexType] = array;
			}
			return array;
		}

		// Token: 0x06002ABD RID: 10941 RVA: 0x0009EA30 File Offset: 0x0009CC30
		private static byte[] GenerateHash(Type hashType, byte[] assemblyBytes)
		{
			byte[] result;
			using (HashAlgorithm hashAlgorithm = HashAlgorithm.Create(hashType.FullName))
			{
				result = hashAlgorithm.ComputeHash(assemblyBytes);
			}
			return result;
		}

		// Token: 0x06002ABE RID: 10942 RVA: 0x0009EA70 File Offset: 0x0009CC70
		private void GenerateDefaultHashes()
		{
			if (this.m_assembly != null)
			{
				this.GenerateDefaultHashes(this.GetRawData());
			}
		}

		// Token: 0x06002ABF RID: 10943 RVA: 0x0009EA8C File Offset: 0x0009CC8C
		private void GenerateDefaultHashes(byte[] assemblyBytes)
		{
			Type[] array = new Type[]
			{
				Hash.GetHashIndexType(typeof(SHA1)),
				Hash.GetHashIndexType(typeof(SHA256)),
				Hash.GetHashIndexType(typeof(MD5))
			};
			foreach (Type type in array)
			{
				Type defaultHashImplementation = Hash.GetDefaultHashImplementation(type);
				if (defaultHashImplementation != null && !this.m_hashes.ContainsKey(type))
				{
					this.m_hashes[type] = Hash.GenerateHash(defaultHashImplementation, assemblyBytes);
				}
			}
		}

		// Token: 0x06002AC0 RID: 10944 RVA: 0x0009EB20 File Offset: 0x0009CD20
		private static Type GetDefaultHashImplementationOrFallback(Type hashAlgorithm, Type fallbackImplementation)
		{
			Type defaultHashImplementation = Hash.GetDefaultHashImplementation(hashAlgorithm);
			if (!(defaultHashImplementation != null))
			{
				return fallbackImplementation;
			}
			return defaultHashImplementation;
		}

		// Token: 0x06002AC1 RID: 10945 RVA: 0x0009EB40 File Offset: 0x0009CD40
		private static Type GetDefaultHashImplementation(Type hashAlgorithm)
		{
			if (hashAlgorithm.IsAssignableFrom(typeof(MD5)))
			{
				if (!CryptoConfig.AllowOnlyFipsAlgorithms)
				{
					return typeof(MD5CryptoServiceProvider);
				}
				return null;
			}
			else
			{
				if (hashAlgorithm.IsAssignableFrom(typeof(SHA256)))
				{
					return Type.GetType("System.Security.Cryptography.SHA256CryptoServiceProvider, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
				}
				return hashAlgorithm;
			}
		}

		// Token: 0x06002AC2 RID: 10946 RVA: 0x0009EB94 File Offset: 0x0009CD94
		private static Type GetHashIndexType(Type hashType)
		{
			Type type = hashType;
			while (type != null && type.BaseType != typeof(HashAlgorithm))
			{
				type = type.BaseType;
			}
			if (type == null)
			{
				type = typeof(HashAlgorithm);
			}
			return type;
		}

		// Token: 0x06002AC3 RID: 10947 RVA: 0x0009EBE4 File Offset: 0x0009CDE4
		private byte[] GetRawData()
		{
			byte[] array = null;
			if (this.m_assembly != null)
			{
				if (this.m_rawData != null)
				{
					array = (this.m_rawData.Target as byte[]);
				}
				if (array == null)
				{
					array = this.m_assembly.GetRawBytes();
					this.m_rawData = new WeakReference(array);
				}
			}
			return array;
		}

		// Token: 0x06002AC4 RID: 10948 RVA: 0x0009EC38 File Offset: 0x0009CE38
		private SecurityElement ToXml()
		{
			this.GenerateDefaultHashes();
			SecurityElement securityElement = new SecurityElement("System.Security.Policy.Hash");
			securityElement.AddAttribute("version", "2");
			foreach (KeyValuePair<Type, byte[]> keyValuePair in this.m_hashes)
			{
				SecurityElement securityElement2 = new SecurityElement("hash");
				securityElement2.AddAttribute("algorithm", keyValuePair.Key.Name);
				securityElement2.AddAttribute("value", Hex.EncodeHexString(keyValuePair.Value));
				securityElement.AddChild(securityElement2);
			}
			return securityElement;
		}

		// Token: 0x06002AC5 RID: 10949 RVA: 0x0009ECE8 File Offset: 0x0009CEE8
		public override string ToString()
		{
			return this.ToXml().ToString();
		}

		// Token: 0x040010F5 RID: 4341
		private RuntimeAssembly m_assembly;

		// Token: 0x040010F6 RID: 4342
		private Dictionary<Type, byte[]> m_hashes;

		// Token: 0x040010F7 RID: 4343
		private WeakReference m_rawData;
	}
}
