using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Course.Persistence;

namespace Course.BusinessLogic
{
    public class Course : ICourse
    {

        private List<string> withoutPreRequisiteCourses;
        private List<string> allCourses;
        private LinkedListNode<string> preRequisiteNode;
        private LinkedListNode<string> courseNode;
        private bool isCourseAlreadyExists;

        private LinkedList<string> orderedCourseList;
        private LinkedListNode<string> currentPreRequisiteNode, currentCourseNode;
        private string[] coursesArray = new string[2];

        public Course()
        {
            orderedCourseList = new LinkedList<string>();
            withoutPreRequisiteCourses = new List<string>();
            allCourses = new List<string>();
        }

        public string OrderCourses(List<string> courseList)
        {
            string responseString = string.Empty;

            try
            {
                foreach (string course in courseList)
                {
                    OrderCourses(course);
                    allCourses.Add(course);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //Add the coures with no pre-requisite in to the ordered course list
            foreach (string strCourseWithoutPre in withoutPreRequisiteCourses)
            {
                if (IndexOfNode(strCourseWithoutPre) == -1)
                {
                    orderedCourseList.AddLast(strCourseWithoutPre);
                }
            }

            SaveCourse();
            if (!isCourseAlreadyExists)
            {
                SaveCourseWithPreRequisite();
            }

            foreach (string courses in orderedCourseList)
            {
                responseString += courses + ", ";               
            }
            return responseString.Trim().TrimEnd(',');
        }


        /// <summary>
        /// This method is used to add nodes to linked list.
        /// </summary>
        /// <param name="inputCourses"></param>
        private void OrderCourses(string inputCourses)
        {
            coursesArray = inputCourses.Split(':');
            coursesArray[0] = coursesArray[0].Trim();
            coursesArray[1] = coursesArray[1].Trim();
            if (coursesArray[1].Equals(string.Empty))
            {
                withoutPreRequisiteCourses.Add(coursesArray[0]);
            }
            else
            {
                //if the linked list is empty
                if (orderedCourseList.Count == 0)
                {
                    orderedCourseList.AddFirst(coursesArray[1]);//to insert pre requisite course
                    preRequisiteNode = orderedCourseList.Find(coursesArray[1]);
                    orderedCourseList.AddLast(coursesArray[0]);
                    courseNode = orderedCourseList.Find(coursesArray[0]);
                }
                else
                {
                    int indexCourseNode = -1;
                    int indexPreRequisiteNode = -1;
                    indexPreRequisiteNode = IndexOfNode(coursesArray[1]);
                    indexCourseNode = IndexOfNode(coursesArray[0]);
                    // If course and pre requisite node doesnot exist in linked list
                    if (indexPreRequisiteNode == -1 && indexCourseNode == -1)
                    {
                        orderedCourseList.AddAfter(preRequisiteNode, coursesArray[1]);
                        preRequisiteNode = orderedCourseList.Find(coursesArray[1]);
                        orderedCourseList.AddBefore(courseNode, coursesArray[0]);
                        courseNode = orderedCourseList.Find(coursesArray[0]);
                    }
                    //If pre requisite node does not exist and course exists in linked list
                    else if (indexPreRequisiteNode == -1 && indexCourseNode != -1)
                    {
                        // This condition is used to check pre requisite course should be ahead of course 
                        if (IndexOfNode(coursesArray[0]) <= IndexOfNode(preRequisiteNode.Value))
                        {
                            currentCourseNode = orderedCourseList.Find(coursesArray[0]);
                            orderedCourseList.AddBefore(currentCourseNode, coursesArray[1]);
                        }
                        else
                        {
                            orderedCourseList.AddAfter(preRequisiteNode, coursesArray[1]);
                            preRequisiteNode = preRequisiteNode.Next;
                        }
                    }
                    else if (indexPreRequisiteNode != -1 && indexCourseNode == -1)
                    {   //If the course becomes pre requiste  and  occurs at almost end or after the course node,
                        //then pre requiste is removed and inserted after pre requisite node and neccessary operations will be performed  
                        if (IndexOfNode(coursesArray[1]) >= IndexOfNode(courseNode.Value))
                        {
                            if (IndexOfNode(coursesArray[1]) == IndexOfNode(courseNode.Value))
                                courseNode = courseNode.Next;
                            currentPreRequisiteNode = orderedCourseList.Find(coursesArray[1]);
                            orderedCourseList.Remove(currentPreRequisiteNode);
                            orderedCourseList.AddAfter(preRequisiteNode, coursesArray[1]);
                            preRequisiteNode = preRequisiteNode.Next;
                            if (courseNode != null)
                            {
                                orderedCourseList.AddBefore(courseNode, coursesArray[0]);
                                courseNode = courseNode.Previous;
                            }
                            else
                            {
                                orderedCourseList.AddLast(coursesArray[0]);
                                courseNode = orderedCourseList.Find(coursesArray[0]);
                            }
                        }
                        else
                        {
                            orderedCourseList.AddBefore(courseNode, coursesArray[0]);
                            courseNode = courseNode.Previous;
                        }
                    }
                    else if (indexCourseNode != -1 && indexPreRequisiteNode != -1)
                    {
                        if (indexPreRequisiteNode >= IndexOfNode(courseNode.Value))
                        {
                            throw new Exception(string.Format("circular dependency exists because of pre requisite {0}", coursesArray[1]));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This method is used to check whether node exists in linked list or not.
        /// </summary>
        /// <param name="courseText"></param>
        /// <returns></returns>
        private int IndexOfNode(string courseText)
        {
            int index = orderedCourseList.Select((n, i) => n == courseText ? (int?)i : null).FirstOrDefault(n => n != null) ?? -1;
            return index;
        }

        public void SaveCourse()
        {
            CourseRepository repo = new CourseRepository();
            List<string> getAllCourses = repo.getAllCourses();

            var abc = getAllCourses.Intersect(orderedCourseList.ToList<string>());
            foreach (string str in abc)
            {
                isCourseAlreadyExists = true;
                return;
            }
            foreach (string course in orderedCourseList)
                repo.AddCourse(course);
        }

        public void SaveCourseWithPreRequisite()
        {
            CourseRepository repo = new CourseRepository();
            string[] courseAlongWithPre = new string[2];
            foreach (string course in this.allCourses)
            {
                courseAlongWithPre = course.Split(':');
                if (!withoutPreRequisiteCourses.Any(str => str.Contains(courseAlongWithPre[0])))
                    repo.AddCoursewithPreRequiste(courseAlongWithPre[0].Trim(), courseAlongWithPre[1].Trim());
                else
                    repo.AddCoursewithPreRequiste(courseAlongWithPre[0], string.Empty);
            }
        }
    }
}
