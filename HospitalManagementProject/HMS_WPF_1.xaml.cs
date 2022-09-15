using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HospitalManagementProject
{
    /// <summary>
    /// Interaction logic for HMS_WPF_1.xaml
    /// </summary>
    public partial class HMS_WPF_1 : Window
    {
        public HMS_WPF_1()
        {
            InitializeComponent();
            HospitalManagementDBEntities db = new HospitalManagementDBEntities();
            var doctors = from d in db.Doctors
                          select new
                          {
                              Name = d.Name,
                              Surname = d.Surname,
                              Specialisation = d.Specialisation
                          };

            foreach (var doctor in doctors)
            {
                Console.WriteLine(doctor.Name);
                Console.WriteLine(doctor.Surname);
                Console.WriteLine(doctor.Specialisation);
            }

            this.gridDoctors.ItemsSource = doctors.ToList();
        }
    }
}
