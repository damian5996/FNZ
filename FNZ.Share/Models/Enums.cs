using System;
using System.Collections.Generic;
using System.Text;

namespace FNZ.Share.Models
{
    public class Enums
    {
        public enum Category
        {
            DogsAdoption,
            CatsAdoption,
            FullfilledDreams,
            FoundationLife
        }

        public enum Type
        {
            Dog,
            Cat
        }

        public enum TabCategory
        {
            AboutFoundation,
            Voluntary
        }

        public enum Action
        {
            Add,
            Edit,
            Delete
        }

        public enum RequestStatus
        {
            InProgress,
            Accepted,
            Refused
        }
    }
}
