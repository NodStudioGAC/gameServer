pipeline {
  agent any

  stages {
    stage ('Docker Build') {
      steps {
        echo 'Building...'
        sh "docker-compose build"
        echo 'Build Finished'
      }
    }
    stage ('Docker Start') {
      steps {
        echo 'Starting...'
        sh "docker-compose up -d"
        echo 'Start Finished'       
      }
    }
  } 
}
