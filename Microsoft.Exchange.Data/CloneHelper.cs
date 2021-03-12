using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Management.Automation;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.AccessControl;
using System.Security.Principal;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000002 RID: 2
	internal class CloneHelper
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public static object SerializeObj(object o)
		{
			if (o == null)
			{
				return null;
			}
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			object result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				binaryFormatter.Serialize(memoryStream, o);
				memoryStream.Seek(0L, SeekOrigin.Begin);
				object obj = binaryFormatter.Deserialize(memoryStream);
				result = obj;
			}
			return result;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000212C File Offset: 0x0000032C
		public static void CloneList(IList target, IList source)
		{
			if (source == null)
			{
				return;
			}
			if (target == null)
			{
				return;
			}
			foreach (object data in source)
			{
				target.Add(CloneHelper.CloneDirectoryData(data));
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000218C File Offset: 0x0000038C
		public static object CloneDirectoryData(object data)
		{
			if (data == null)
			{
				return null;
			}
			if (data.GetType() == typeof(RawSecurityDescriptor))
			{
				int binaryLength = ((RawSecurityDescriptor)data).BinaryLength;
				byte[] binaryForm = new byte[binaryLength];
				((RawSecurityDescriptor)data).GetBinaryForm(binaryForm, 0);
				return new RawSecurityDescriptor(binaryForm, 0);
			}
			if (data is ICloneable)
			{
				return ((ICloneable)data).Clone();
			}
			if (data.GetType() == typeof(SecurityIdentifier) || data.GetType() == typeof(PSCredential))
			{
				return data;
			}
			ImmutableObjectAttribute immutableObjectAttribute = (ImmutableObjectAttribute)data.GetType().GetTypeInfo().GetCustomAttribute(typeof(ImmutableObjectAttribute));
			if (immutableObjectAttribute != null && immutableObjectAttribute.Immutable)
			{
				return data;
			}
			object result;
			try
			{
				result = CloneHelper.SerializeObj(data);
			}
			catch (SerializationException)
			{
				throw new CannotCloneException(DataStrings.DataNotCloneable(data.GetType().Name));
			}
			return result;
		}
	}
}
