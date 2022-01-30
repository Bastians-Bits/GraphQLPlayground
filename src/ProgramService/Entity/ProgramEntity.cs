using System;
using System.ComponentModel.DataAnnotations;

namespace ProgramService.Entity
{
	public class ProgramEntity
	{
		[Key]
		public string Name { get; set; } = string.Empty;
		public string Homepage { get; set; } = string.Empty;
		public IList<VersionEntity> Version { get; set; } = new List<VersionEntity>();
    }
}

