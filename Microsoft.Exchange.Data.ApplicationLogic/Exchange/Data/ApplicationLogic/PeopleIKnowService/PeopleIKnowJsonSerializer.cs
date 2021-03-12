using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.PeopleIKnowService
{
	// Token: 0x02000186 RID: 390
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class PeopleIKnowJsonSerializer : IPeopleIKnowSerializer
	{
		// Token: 0x06000F27 RID: 3879 RVA: 0x0003D216 File Offset: 0x0003B416
		private PeopleIKnowJsonSerializer()
		{
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x0003D220 File Offset: 0x0003B420
		public string Serialize(PeopleIKnowGraph peopleIKnowGraph)
		{
			ArgumentValidator.ThrowIfNull("peopleIKnowGraph", peopleIKnowGraph);
			DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(PeopleIKnowGraph), PeopleIKnowJsonSerializer.KnownTypes);
			string @string;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				dataContractJsonSerializer.WriteObject(memoryStream, peopleIKnowGraph);
				@string = Encoding.UTF8.GetString(memoryStream.ToArray());
			}
			return @string;
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x0003D28C File Offset: 0x0003B48C
		public PeopleIKnowGraph Deserialize(string serializedPeopleIKnow)
		{
			DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(PeopleIKnowGraph), PeopleIKnowJsonSerializer.KnownTypes);
			PeopleIKnowGraph result;
			using (MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(serializedPeopleIKnow)))
			{
				result = (PeopleIKnowGraph)dataContractJsonSerializer.ReadObject(memoryStream);
			}
			return result;
		}

		// Token: 0x040007FB RID: 2043
		private static readonly Type[] KnownTypes = new Type[]
		{
			typeof(RelevantPerson[]),
			typeof(RelevantPerson)
		};

		// Token: 0x040007FC RID: 2044
		internal static readonly PeopleIKnowJsonSerializer Singleton = new PeopleIKnowJsonSerializer();
	}
}
