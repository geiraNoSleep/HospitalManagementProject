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
                              Specialization = d.Specialization
                          };

            this.gridDoctors.ItemsSource = doctors.ToList();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            HospitalManagementDBEntities db = new HospitalManagementDBEntities();

            Doctors doctorObject = new Doctors()
            {
                Name = txtName.Text,
                Surname = txtSurname.Text,
                Specialization = txtSpecialization.Text
            };

            db.Doctors.Add(doctorObject);
            db.SaveChanges();
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            HospitalManagementDBEntities db = new HospitalManagementDBEntities();

            this.gridDoctors.ItemsSource = db.Doctors.ToList();
        }

        private int updatingDoctorID = 0;
        private void gridDoctors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.gridDoctors.SelectedIndex >= 0)
            {
                if (this.gridDoctors.SelectedItems.Count >= 0)
                {
                    if (this.gridDoctors.SelectedItems[0].GetType() == typeof(Doctors))
                    {
                        Doctors d = (Doctors)this.gridDoctors.SelectedItems[0];
                        this.txtName2.Text = d.Name;
                        this.txtSurname2.Text = d.Surname;
                        this.txtSpecialization2.Text = d.Specialization;
                        
                        this.updatingDoctorID = d.Id;
                    }
                }
            }
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            HospitalManagementDBEntities db = new HospitalManagementDBEntities();

            var x = from d in db.Doctors
                    where d.Id == this.updatingDoctorID
                    select d;

            Doctors obj = x.SingleOrDefault();

            if (obj != null)
            {
                obj.Name = this.txtName2.Text;
                obj.Surname = this.txtSurname2.Text;
                obj.Specialization = this.txtSpecialization2.Text;

                db.SaveChanges();
            }

        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            HospitalManagementDBEntities db = new HospitalManagementDBEntities();

            var x = from d in db.Doctors
                    where d.Id == this.updatingDoctorID
                    select d;

            Doctors obj = x.SingleOrDefault();

            if (obj != null)
            {
                db.Doctors.Remove(obj);
                db.SaveChanges();
            }


        }
    }
}
