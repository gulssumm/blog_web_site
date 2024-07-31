pipeline {
  agent any
  stages {
    stage ('Build') {
      steps {
        sh 'dotnet build'
      }
    }
     stage ('LIST OF TOOLS') {
      steps {
        sh 'dotnet tool list -g'
      }
    }
    stage ('dotnet install ef') {
      steps {
        sh 'dotnet tool install --global dotnet-ef'
      }
    }
   stage ('ADD EF TO PATH') {
      steps {
        sh 'export PATH="$PATH:/var/lib/jenkins/.dotnet/tools"'
      }
    }
    stage ('LIST OF TOOLS') {
      steps {
        sh 'dotnet tool list -g'
      }
    }
    stage ('Migration') {
      steps {
        sh 'dotnet-ef database update --project "blog_website/blog_website.csproj"'
      }
    }
    stage ('Restore') {
      steps {
        sh 'dotnet restore'
      }
    }
    stage ('Run') {
      steps {
        sh 'dotnet run'
      }
    }
  }
}
