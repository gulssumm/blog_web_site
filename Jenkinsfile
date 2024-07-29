pipeline {
  agent any
  environment {
        DB_CONNECTION_STRING = credentials('db_connection_string')
  }

  stages {
    stage ('Initialize') {
      steps {
        echo 'GULSUM'
      }
    }
    stage ('Build') {
      steps {
        bat "run.cmd"
      }
    }
    stage('Checkout') {
            steps {
                git url: 'https://github.com/github.com/blog_web_site.git', branch: 'main'
            }
        }

        stage('Restore') {
            steps {
                sh 'dotnet restore'
            }
        }

        stage('Build') {
            steps {
                sh 'dotnet build --configuration Release'
            }
        }

        stage('Publish') {
            steps {
                sh 'dotnet publish --configuration Release --output ./publish'
            }
        }

        stage('Deploy') {
            steps {
                // Deploy to your local environment or server
                // This can be specific to your setup
                // For example, copying the files to a server or running a script
                sh 'cp -r ./publish /path/to/deployment'
            }
        }

        stage('Run Tests') {
            steps {
                sh 'dotnet test'
            }
        }
  }
}